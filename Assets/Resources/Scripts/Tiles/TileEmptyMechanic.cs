using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEmptyMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TerrainGenerator.instance.canMove)
        {
            this.gameObject.transform
                .Translate(Vector3.forward * Time.deltaTime * TerrainGenerator.instance.velocity);
        }
    }
}
