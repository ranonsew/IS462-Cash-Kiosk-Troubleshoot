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
	flag.StringVar(&obsPort, "port", "45", "OBS WebSocket Port")
	flag.StringVar(&obsPass, "password", "pwd", "OBS WebSocket Password")
	flag.Parse()

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
	// add OBS-related Handler functions
	mux.HandleFunc("GET /version/obs", GetVersion)
	mux.HandleFunc("GET /scene/all", GetAllScenes)
	mux.HandleFunc("GET /scene/current", GetCurrentSceneName)
	mux.HandleFunc("POST /scene/set/{name}", ChangeCurrentScene)
	mux.HandleFunc("GET /directory", GetCurrentRecordingDirectory)
	mux.HandleFunc("POST /directory/set/{dir}", SetNewRecordingDirectory)
	mux.HandleFunc("POST /record/start", StartRecording)
	mux.HandleFunc("POST /record/stop", StopRecording)

	// Run server
	fmt.Println("HTTP Server started at http://127.0.0.1:45713")
	fmt.Println("Press Ctrl+C to exit...")
	if err := http.ListenAndServe(":45713", mux); err != nil {
		fmt.Printf("Error: %s\n", err.Error())
	}
}
