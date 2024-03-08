package main

import (
	"encoding/json"
	"fmt"
	"net/http"

	"github.com/andreykaipov/goobs/api/requests/config"
	"github.com/andreykaipov/goobs/api/requests/scenes"
)

/*
	Handler Functions which are used in the Mux (multiplexer)
*/

// Show version info of OBS because why not
func GetVersion(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	version, err := client.General.GetVersion()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Printf("OBS Studio version: %s\n", version.ObsVersion)
	// fmt.Printf("Server protocol version: %s\n", version.ObsWebSocketVersion)

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"data": version})
}

// get all available scenes
func GetAllScenes(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Scenes.GetSceneList()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// loop through all the available scenes and print out
	// var scenes string
	// for i, s := range res.Scenes {
	// 	fmt.Printf("%d - Index: %d, Name: %s\n", i, s.SceneIndex, s.SceneName)
	// }

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"data": res})
}

// get the current program scene
func GetCurrentSceneName(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Scenes.GetCurrentProgramScene()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Printf("Current Scene: %s - %s\n", res.CurrentProgramSceneUuid, res.CurrentProgramSceneName)
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"data": res})
}

// switch to a specific scene via its name
func ChangeCurrentScene(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	name := r.PathValue("name")
	newScene := &scenes.SetCurrentProgramSceneParams{SceneName: &name}

	_, err := client.Scenes.SetCurrentProgramScene(newScene)
	if err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Printf("Current Program Scene switched to: %s\n", name)
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Current Program Scene switched to: %s", name)})
}

// Get the current recording output directory
func GetCurrentRecordingDirectory(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Config.GetRecordDirectory()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Printf("Recording Output Directory: %s\n", res.RecordDirectory)
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"data": res})
}

// Set a new recording output directory
func SetNewRecordingDirectory(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	dir := r.PathValue("dir")
	newDir := &config.SetRecordDirectoryParams{RecordDirectory: &dir}

	_, err := client.Config.SetRecordDirectory(newDir)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Printf("Recording Output Directory set to: %s\n", dir)
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Recording Output Directory set to: %s", dir)})
}

// start a new OBS recording
func StartRecording(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	_, err := client.Record.StartRecord()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Println("Recording Started!")
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": "Recording started!"})
}

// stop the OBS recording and save to the current recording output directory
func StopRecording(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Record.StopRecord()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	// fmt.Printf("Recording stopped! Recording saved to: %s\n", res.OutputPath)
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Recording stopped! Recording saved to: %s", res.OutputPath)})
}
