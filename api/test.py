import requests

baseURL = "http://localhost:45713"

# chatgpt ahh function, idk if this is the right way

def test1():
  # test data
  sceneName = "python_test1"
  inputKind = "monitor_capture"
  inputName = "pyInp1"
  sceneItemEnabled = True
  inputPropertyName = "monitor_id"

  try:
    # 0.5. Check for scene

    # 1. Create a new scene
    res1 = requests.post(f"{baseURL}/scene/create", json={"SceneName": sceneName})
    res1.raise_for_status() # Check if the request was successful
    print("Scene Created", end="\n")

    # 2. Change to the new scene
    res2 = requests.get(f"{baseURL}/scene/change/" + sceneName)
    res2.raise_for_status() # Check if the request was successful
    print("Scene Changed", end="\n")

    # 2.5 Check for input (if have, skip to 4.)

    # 3. Add an input (source --> display capture in this example)
    res3 = requests.post(f"{baseURL}/input/create", json={"SceneName": sceneName, "InputKind": inputKind, "InputName": inputName, "SceneItemEnabled": sceneItemEnabled})
    res3.raise_for_status() # Check if the request was successful
    print("Display Capture Added", end="\n")

    # 4. Get the properties (monitor id in this example)
    res4 = requests.get(f"{baseURL}/input/{inputName}/properties/{inputPropertyName}")
    res4.raise_for_status() # Check if the request was successful
    monitor_id = res4.json()["propertyItems"][0]["itemValue"]
    print(f"Monitor ID: {monitor_id}", end="\n")

    # 4.5 or 3.5, check for settings

    # 5. Change the settings with a selected property
    res5 = requests.post(f"{baseURL}/input/settings", json={"InputName": inputName, "InputSettings": {inputPropertyName: monitor_id}})
    res5.raise_for_status() # Check if the request was successful
    print("Check OBS", end="\n")

  except requests.exceptions.HTTPError as errh:
    print("HTTP ERR: " + errh)
  except requests.exceptions.ConnectionError as errc:
    print("CONN ERR: " + errc)
  except requests.exceptions.Timeout as errt:
    print("TIMEOUT: " + errt)
  except requests.exceptions.RequestException as err:
    print("REQ EXP: " + err)

if __name__ == "__main__":
  test1()
