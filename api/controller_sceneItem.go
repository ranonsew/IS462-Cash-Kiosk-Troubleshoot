package main

import (
	"net/http"
	"encoding/json"

	"github.com/andreykaipov/goobs/api/requests/sceneitems"
)

/*
	SceneItem Handler Functions
*/

// Get list of scene items (needs scene name or uuid)
func GetSceneItems(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	name := r.PathValue("name")

	res, err := client.SceneItems.GetSceneItemList(&sceneitems.GetSceneItemListParams{SceneName: &name})
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}

// create a scene item - {SceneName: string, SceneItemEnabled: bool, SourceName: string}
func CreateNewSceneItem(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var tmp sceneitems.CreateSceneItemParams
	if err := json.NewDecoder(r.Body).Decode(&tmp); err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	res, err := client.SceneItems.CreateSceneItem(&tmp)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusCreated)
	json.NewEncoder(w).Encode(res)
}
