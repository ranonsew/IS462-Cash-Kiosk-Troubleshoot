package main

import (
	"encoding/json"
	"net/http"

	// "github.com/andreykaipov/goobs/api/requests/ui"
)

/*
	UI Handler Functions
*/

func GetMonitorList(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	res, err := client.Ui.GetMonitorList()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(w).Encode(map[string]any{"message": err.Error()})
		return
	}

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]any{"data": res})
}
