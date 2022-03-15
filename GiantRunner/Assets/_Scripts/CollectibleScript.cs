using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color == transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_Color"))
            {
                PlayerController.instance.SizeUp();
                Destroy(this.gameObject);
                BossScript.instance._bossfly += 0.7f;
            }
            else
            {
                other.transform.localScale = new Vector3(other.transform.localScale.x - 0.02f, other.transform.localScale.y - 0.02f, other.transform.localScale.z - 0.02f);
                Destroy(this.gameObject);
            }
        }
 
    }
}
