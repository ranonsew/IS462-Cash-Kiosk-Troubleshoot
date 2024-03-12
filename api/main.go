package main

import (
	"flag"
	"fmt"
	"net/http"
	"os"

	"github.com/andreykaipov/goobs"
	"github.com/andreykaipov/goobs/api/events"
)

var (
	client  *goobs.Client // more global client initialization
	err     error         // for initial error handling with client
	obsPort string        // port
	obsPass string        // password passthrough
)

func main() {
	// flags for passing in values in run.bat after exe/binary compilation
	flag.StringVar(&obsPort, "port", "45", "OBS WebSocket Port")
	flag.StringVar(&obsPass, "password", "pwd", "OBS WebSocket Password")
	flag.Parse()

	// connect to OBS WebSocket Server
	client, err = goobs.New(fmt.Sprintf("localhost:%s", obsPort), goobs.WithPassword(obsPass))
	if err != nil {
		fmt.Fprintf(os.Stderr, "Error connecting to OBS Websocket: %s\n", err.Error())
		os.Exit(1)
	}

	// defer the disconnect to later
	defer client.Disconnect()
	fmt.Println("Connected to obs")

	// Goroutine to listen for OBS events
	go func() {
		client.Listen(func(event any) {
			switch e := event.(type) {
			case *events.CurrentProgramSceneChanged:
				fmt.Printf("Current scene changed to: %s\n", e.SceneName)
			case *events.RecordStateChanged:
				fmt.Printf("Recording? %t.\nRecording State changed to: %s\n", e.OutputActive, e.OutputState)
			case *events.ExitStarted:
				fmt.Println("Exit started")
			default:
				fmt.Printf("unhandled: %T\n", event)
			}
		})
	}()

	// start Mux Server
	mux := http.NewServeMux()

	// add OBS general Handler functions
	mux.HandleFunc("GET /version/obs", GetVersion)

	// add Scene Handler functions
	mux.HandleFunc("GET /scene/all", GetAllScenes)
	mux.HandleFunc("GET /scene/current", GetCurrentSceneName)
	mux.HandleFunc("GET /scene/change/{name}", ChangeCurrentScene)
	mux.HandleFunc("POST /scene/create", CreateNewScene) // {"SceneName": ""}

	// add Input Handler functions
	mux.HandleFunc("GET /input/kinds", GetInputKindList)
	mux.HandleFunc("GET /input/{kind}", GetInputList)
	mux.HandleFunc("POST /input/create", CreateNewInput) // {"SceneName": "", "InputKind": "", "InputName": "", "SceneItemEnabled": true}
	mux.HandleFunc("GET /input/settings/{inputName}", GetCurrentInputSettings)
	mux.HandleFunc("POST /input/settings", SetCurrentInputSettings) // {"InputName": "", "InputSettings": {"key": ""}}
	mux.HandleFunc("GET /input/{inputName}/properties/{propertyName}", GetInputPropertiesListPropertyItems)

	// add Monitor Handler functions
	mux.HandleFunc("GET /monitor/all", GetMonitorList)

	// add SceneItem Handler functions
	mux.HandleFunc("GET /sceneItems/{name}", GetSceneItems)
	mux.HandleFunc("POST /sceneItems/create", CreateNewSceneItem) // {"SceneName": "", "SceneItemEnabled": true, "SourceName": ""}

	// add RecordingDirectory Handler functions
	mux.HandleFunc("GET /directory", GetCurrentRecordingDirectory)
	mux.HandleFunc("POST /directory", SetNewRecordingDirectory) // {"RecordDirectory": ""}

	// add Record Handler functions
	mux.HandleFunc("POST /record/start", StartRecording)
	mux.HandleFunc("POST /record/pause", PauseRecording)
	mux.HandleFunc("POST /record/resume", ResumeRecording)
	mux.HandleFunc("POST /record/stop", StopRecording)

	// Run server
	fmt.Println("HTTP Server started at http://127.0.0.1:45713")
	// fmt.Println("Press Ctrl+C to exit...")
	if err := http.ListenAndServe(":45713", mux); err != nil {
		fmt.Printf("Error: %s\n", err.Error())
	}
}
