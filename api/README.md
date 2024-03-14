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

### API Routes

GET /version/obs
- get version of OBS (that we connected to)

GET /scene/all
- get list of all scenes
GET /scene/current
- get current scene
GET /scene/change/{name}
- change to scene {name}
POST /scene/create --json '{"SceneName": ""}'
- create new scene

GET /input/kinds
- get list of input kinds (source types)
GET /input/{kind}
- get details of specific kind of input
POST /input/create --json '{"SceneName": "", "InputKind": "", "InputName": "", "SceneItemEnabled": true}'
- create a new input kind for a scene
GET /input/settings/{inputName}
- get input settings for a specific input (source)
POST /input/settings --json '{"InputName": "", "InputSettings": {"key": ""}}'
- edit settings of a specific input
GET /input/{inputName}/properties/{propertyName}
- get the properties of a specific input (based on a property name)

GET /sceneItems/{name}
- get the scene items of a scene
POST /sceneItems/create --json '{"SceneName": "", "SceneItemEnabled": true, "SourceName": ""}'
- create a new scene item for a scene

GET /directory
- get recording output directory
POST /directory --json '{"RecordDirectory": ""}'
- set recording output directory

POST /record/start
- start recording
POST /record/pause
- pause recording
POST /record/resume
- resume recording
POST /record/stop
- stop recording

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
