package main

import (
	"fmt"
	"net/http"
	"encoding/json"

	"github.com/andreykaipov/goobs/api/requests/scenes"
)

/*
	Scene Handler Functions
*/

// get all available scenes
func GetAllScenes(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Scenes.GetSceneList()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

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

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"data": res})
}

// switch to a specific scene via its name
func ChangeCurrentScene(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	name := r.PathValue("name")
	params := scenes.NewSetCurrentProgramSceneParams().WithSceneName(name)
	_, err := client.Scenes.SetCurrentProgramScene(params)
	if err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Current Program Scene switched to: %s", name)})
}

// create a new scene - {SceneName: string}
func CreateNewScene(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var tmp scenes.CreateSceneParams
	if err := json.NewDecoder(r.Body).Decode(&tmp); err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	res, err := client.Scenes.CreateScene(&tmp)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusCreated)
	json.NewEncoder(w).Encode(map[string]any{"data": res})
}
