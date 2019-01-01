using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HGameController : MonoBehaviour {

    public int level;
    [SerializeField] Map map;
    [SerializeField] List<Material> skybox;
    [SerializeField] Edit_CSV keyName;

    void Start () {
        level = 1;
        map.GenerateLevel(level);
        HPlayerControl.check = true;
        int randSkybox;

        randSkybox = Random.Range(0, 5);
        RenderSettings.skybox = skybox[randSkybox];
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("s"))
        {
            keyName.delete_name();
            SceneManager.LoadScene("Start");
        }
    }

    public void LevelUp()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameBoard"));
        level++;
        map.GenerateLevel(level);
    }
}
