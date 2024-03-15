import requests

# chatgpt ahh function, idk if this is the right way
if __name__ == "__main__":
  try:
    res = requests.get("http://localhost:45713/connect-display-capture")
    res.raise_for_status()
    print(res.json(), end="\n")

  except requests.exceptions.HTTPError as errh:
    print("HTTP ERR: " + errh)
  except requests.exceptions.ConnectionError as errc:
    print("CONN ERR: " + errc)
  except requests.exceptions.Timeout as errt:
    print("TIMEOUT: " + errt)
  except requests.exceptions.RequestException as err:
    print("REQ EXP: " + err)
