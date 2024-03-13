package main

import (
	"encoding/json"
	"net/http"

	"github.com/andreykaipov/goobs/api/requests/inputs"
)

/*
	Input Handler Functions (considered as sources in OBS GUI)
*/

// Get input kind list
func GetInputKindList(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Inputs.GetInputKindList()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}

// Get input list
func GetInputList(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	kind := r.PathValue("kind")
	params := inputs.NewGetInputListParams().WithInputKind(kind)
	res, err := client.Inputs.GetInputList(params)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}

// Create new Input - {SceneName: string, InputKind: string, InputName: string, SceneItemEnabled: bool}
func CreateNewInput(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var tmp inputs.CreateInputParams
	if err := json.NewDecoder(r.Body).Decode(&tmp); err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	res, err := client.Inputs.CreateInput(&tmp)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusCreated)
	json.NewEncoder(w).Encode(res)
}

// Get the input settings
func GetCurrentInputSettings(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	inputName := r.PathValue("inputName")
	params := inputs.NewGetInputSettingsParams().WithInputName(inputName)
	res, err := client.Inputs.GetInputSettings(params)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}

// Set the input settings
func SetCurrentInputSettings(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	var tmp inputs.SetInputSettingsParams
	if err := json.NewDecoder(r.Body).Decode(&tmp); err != nil {
		w.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	_, err := client.Inputs.SetInputSettings(&tmp)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"message": "Settings set successfully"})
}

// Get the input properties list for the given input (so we can set the settings).
// it's really long, idk why
func GetInputPropertiesListPropertyItems(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	inputName := r.PathValue("inputName")
	propertyName := r.PathValue("propertyName")
	params := inputs.NewGetInputPropertiesListPropertyItemsParams().WithInputName(inputName).WithPropertyName(propertyName)
	res, err := client.Inputs.GetInputPropertiesListPropertyItems(params)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}
