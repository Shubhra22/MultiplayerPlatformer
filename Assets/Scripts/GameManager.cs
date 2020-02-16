using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnerCanvas;
    public GameObject playerCamera;
    public Button spawnBtn;
    // Start is called before the first frame update
    void Awake()
    {
        spawnerCanvas.SetActive(true);
        spawnBtn.onClick.AddListener(SpawnPlayerWithCam);
    }

    public void SpawnPlayerWithCam()
    {
        float randomPos = Random.Range(-5, 5);
        var position = playerPrefab.transform.position;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(randomPos*position.x, position.y),
            Quaternion.identity);

        
        var spawnedPos = player.transform.position;
        GameObject cam = PhotonNetwork.Instantiate(playerCamera.name, new Vector3(spawnedPos.x, spawnedPos.y,-10),
            Quaternion.identity);

        player.GetComponent<Player>().cam = cam.GetComponent<Camera>();
        cam.GetComponent<CameraMove>()._target = player.transform;

        spawnerCanvas.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
