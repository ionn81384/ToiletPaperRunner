using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnHitAndDestroy : MonoBehaviour
{
    public GameObject GTerrain;
    private void Start()
    {
        GTerrain = GameObject.Find("GameTerraingenerator");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "RepeatTile")
        {
            GTerrain.GetComponent<TerrainGenerator>().PlaceOneTile();
            Destroy(this.gameObject);
        }
    }
}
