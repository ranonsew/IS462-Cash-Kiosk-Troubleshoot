package main

import (
	"encoding/json"
	"fmt"
	"net/http"

	"github.com/andreykaipov/goobs/api/requests/sceneitems"
)

/*
	SceneItem Handler Functions
*/

// Get list of scene items (needs scene name or uuid)
// @returns {"sceneItems": [{"inputKind": string, "isGroup": bool, "sceneItemBlendMode": string, "sceneItemEnabled": bool, "sceneItemId": int, "sceneItemIndex": int, "sceneItemLocked": bool, "sceneItemTransform": SceneItemTransform, "sourceName": string, "sourceType": string
func GetSceneItems(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	name := r.PathValue("sceneName")
	params := sceneitems.NewGetSceneItemListParams().WithSceneName(name)
	res, err := client.SceneItems.GetSceneItemList(params)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}

// create a scene item - {sceneName: string, sceneItemEnabled: bool, sourceName: string}
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

// set scene item enabled state - {sceneName: string, sceneItemEnabled: bool, sceneItemId: int}
// @returns {"message": string}
func SetSceneItemEnabled(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var tmp sceneitems.SetSceneItemEnabledParams
	if err := json.NewDecoder(r.Body).Decode(&tmp); err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	_, err := client.SceneItems.SetSceneItemEnabled(&tmp)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": fmt.Sprintf("Set scene item %s to %t", *tmp.SceneName, *tmp.SceneItemEnabled)})
}
