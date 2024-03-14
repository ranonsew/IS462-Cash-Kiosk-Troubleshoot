package main

import (
	"fmt"
	"net/http"
	"encoding/json"

	"github.com/andreykaipov/goobs/api/requests/config"
)

/*
	RecordingDirectory Handler Functions
*/

// Get the current recording output directory
func GetCurrentRecordingDirectory(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Config.GetRecordDirectory()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(res)
}

// Set a new recording output directory - {RecordDirectory: string}
func SetNewRecordingDirectory(w http.ResponseWriter, r *http.Request) {
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
}
