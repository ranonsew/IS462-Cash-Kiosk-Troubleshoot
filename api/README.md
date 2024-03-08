# ConnectToOBS API

## Usage Instructions

### OBS Studio

- recording is done via OBS Studio, so it needs to be installed via the website: https://obsproject.com/

### run.bat editing

- port & password
  - from OBS Studio: "Tools > WebSocket Server Settings"
  - Edit the port or keep it default (4455). Generate a new password or create your own, then copy paste it into the variables section
- Install Directory of OBS Studio
  - Edit the "C:\Program Files\obs-studio\bin\64bit" if obs64.exe is installed in a different directory
  - should usually be default installed here, if not, it may be installed in a different drive, where it might instead be prefixed with "D" or "E" instead of "C"

---

## Development Instructions (assuming windows)

### Go

- Need to have Go 1.22 installed
- use the following commands to build the executable if you don't want to use makefile,
  - To build: go build -o ConnectToOBS.exe .
  - To run (without .bat file): ./ConnectToOBS.exe

### Makefile (only if you want to, optional)

- needs to have mingw installed

### .bat

- ideally, to test like user, open file explorer and double click on run.bat
