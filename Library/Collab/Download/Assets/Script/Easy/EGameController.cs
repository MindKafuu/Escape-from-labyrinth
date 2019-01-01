using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGameController : MonoBehaviour {

    [SerializeField] Map map;
    [SerializeField] GameObject spawnObject;
    [SerializeField] int maximunObject = 3;
    [SerializeField] List<Material> skybox;

    public int level;
    private int objectCount;

    void Start () {
        level = 1;
        map.GenerateLevel(level);
        objectCount = 0;

        int randSkybox;

        randSkybox = Random.Range(0, 5);
        RenderSettings.skybox = skybox[randSkybox];

    }
	
	// Update is called once per frame
	void Update () {
        
        if (objectCount < maximunObject)
        {
            int randX, randZ;

            do
            {
                randX = Random.Range(0, map.tileMap.Length);
                randZ = Random.Range(0, map.tileMap[0].Length);

            } while (map.tileMap[randX][randZ] != '0');

            GameObject spawnedObject = GameObject.Instantiate(spawnObject, new Vector3(randX, 0.5f, randZ), spawnObject.transform.rotation);
            spawnedObject.GetComponent<EnemyController>().SetPosition(randX, randZ);
            objectCount++;
        }

    }

    public void LevelUp()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameBoard"));
        level++;
        map.GenerateLevel(level);
    }
}
