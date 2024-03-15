package main

import (
	"encoding/json"
	"flag"
	"fmt"
	"log"
	"net/http"
	"os"
	"reflect"
	"strings"

	"github.com/andreykaipov/goobs"
	"github.com/andreykaipov/goobs/api/events"
	"github.com/andreykaipov/goobs/api/requests/config"
	"github.com/andreykaipov/goobs/api/requests/inputs"
	"github.com/andreykaipov/goobs/api/requests/sceneitems"
	"github.com/andreykaipov/goobs/api/requests/scenes"
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
	flag.StringVar(&obsPass, "password", "hmr6zCE8jsk7IiEK", "OBS WebSocket Password")
	flag.Parse()

	// connect to OBS WebSocket Server
	client, err = goobs.New(fmt.Sprintf("10.124.31.120:%s", obsPort), goobs.WithPassword(obsPass))
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
			case *events.InputCreated:
				fmt.Printf("Input %s created\n", e.InputName)
			default:
				fmt.Printf("unhandled: %T\n", event)
			}
		})
	}()

	// start Mux Server
	mux := http.NewServeMux()

	// Display Capture Handler
	mux.HandleFunc("GET /connect-display-capture", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")
		sceneName := "Unity_SceneCapture"
		inputName := "Unity_InputCapture"
		inputKind := "monitor_capture"
		propertyName := "monitor_id"

		sceneList, err := client.Scenes.GetSceneList()
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}
		fmt.Println("Got scene list")

		hasScene := false
		for _, scene := range sceneList.Scenes {
			if scene.SceneName == sceneName {
				hasScene = true
				break
			}
		}

		if !hasScene {
			createSceneParams := scenes.NewCreateSceneParams().WithSceneName(sceneName)
			_, err := client.Scenes.CreateScene(createSceneParams)
			if err != nil {
				w.WriteHeader(http.StatusInternalServerError)
				json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
				return
			}
			fmt.Println("Created Scene")
		}

		changeSceneParams := scenes.NewSetCurrentProgramSceneParams().WithSceneName(sceneName)
		_, err = client.Scenes.SetCurrentProgramScene(changeSceneParams)
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		sceneItemListParams := sceneitems.NewGetSceneItemListParams().WithSceneName(sceneName)
		sceneItemList, err := client.SceneItems.GetSceneItemList(sceneItemListParams)
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		inputNameExists := false
		inputKindExists := false
		sceneItemEnabled := false
		sceneItemId := 0
		if sceneItemList.SceneItems != nil {
			for _, sceneItem := range sceneItemList.SceneItems {
				if sceneItem.SourceName == inputName {
					inputNameExists = true
					if sceneItem.InputKind == inputKind {
						inputKindExists = true
						sceneItemEnabled = sceneItem.SceneItemEnabled
						sceneItemId = sceneItem.SceneItemID
						break
					}
					break // break if sourceName is inputName, but incorrect inputKind
				}
			}
		}

		if inputKindExists {
			if !sceneItemEnabled {
				setSceneItemEnabledParams := sceneitems.NewSetSceneItemEnabledParams().WithSceneName(sceneName).WithSceneItemId(sceneItemId).WithSceneItemEnabled(true)
				_, err := client.SceneItems.SetSceneItemEnabled(setSceneItemEnabledParams)
				if err != nil {
					w.WriteHeader(http.StatusInternalServerError)
					json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
					return
				}
			}
		} else {
			if inputNameExists {
				removeInputParams := inputs.NewRemoveInputParams().WithInputName(inputName)
				_, err := client.Inputs.RemoveInput(removeInputParams)
				if err != nil {
					w.WriteHeader(http.StatusInternalServerError)
					json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
					return
				}
			}

			createInputParams := inputs.NewCreateInputParams().WithSceneName(sceneName).WithInputName(inputName).WithInputKind(inputKind).WithSceneItemEnabled(true)
			_, err := client.Inputs.CreateInput(createInputParams)
			if err != nil {
				w.WriteHeader(http.StatusInternalServerError)
				json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
				return
			}
		}

		getInputPropertiesListPropertyItemsParams := inputs.NewGetInputPropertiesListPropertyItemsParams().WithInputName(inputName).WithPropertyName(propertyName)
		inputPropertiesListPropertyItems, err := client.Inputs.GetInputPropertiesListPropertyItems(getInputPropertiesListPropertyItemsParams)
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}
		monitor_id := inputPropertiesListPropertyItems.PropertyItems[0].ItemValue

		setInputSettingsParams := inputs.NewSetInputSettingsParams().WithInputName(inputName).WithInputSettings(map[string]any{propertyName: monitor_id})
		_, err = client.Inputs.SetInputSettings(setInputSettingsParams)
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(map[string]any{"message": "Connected OBS display capture successfully!"})
	})

	// add RecordingDirectory Handler functions
	mux.HandleFunc("GET /directory", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		res, err := client.Config.GetRecordDirectory()
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		files, err := os.ReadDir(res.RecordDirectory)
		if err != nil {
			log.Fatal(err)
		}

		var s []string
		for _, file := range files {
			fmt.Println(file.Name(), file.IsDir())
			if strings.HasSuffix(file.Name(), ".mov") {
				s = append(s, file.Name())
			}
		}
		fmt.Printf("%+v\n", res.RecordDirectory)

		fmt.Println(reflect.TypeOf(res))
		w.WriteHeader(http.StatusOK)
		// json.NewEncoder(w).Encode(res)
		json.NewEncoder(w).Encode(map[string]any{"directory": res.RecordDirectory,
			"files": s})
	})
	mux.HandleFunc("POST /directory", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		var tmp config.SetRecordDirectoryParams
		if err := json.NewDecoder(r.Body).Decode(&tmp); err != nil {
			w.WriteHeader(http.StatusBadRequest)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		_, err := client.Config.SetRecordDirectory(&tmp)
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Recording Output Directory set to: %s", *tmp.RecordDirectory)})
	}) // {"recordDirectory": ""}

	// add Record Handler functions
	mux.HandleFunc("POST /record/start", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		_, err := client.Record.StartRecord()
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(map[string]any{"message": "Recording started!"})
	})
	mux.HandleFunc("POST /record/pause", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		_, err := client.Record.PauseRecord()
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(map[string]any{"message": "Recording paused!"})
	})
	mux.HandleFunc("POST /record/resume", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		_, err := client.Record.ResumeRecord()
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(map[string]any{"message": "Recording resumed!"})
	})
	mux.HandleFunc("POST /record/stop", func(w http.ResponseWriter, r *http.Request) {
		w.Header().Set("Content-Type", "application/json")

		res, err := client.Record.StopRecord()
		if err != nil {
			w.WriteHeader(http.StatusInternalServerError)
			json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
			return
		}

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Recording stopped! Saved to: %s", res.OutputPath)})
	})

	// Run server
	fmt.Println("HTTP Server started at http://127.0.0.1:45713")
	// fmt.Println("Press Ctrl+C to exit...")
	if err := http.ListenAndServe(":45713", mux); err != nil {
		fmt.Printf("Error: %s\n", err.Error())
	}
}
