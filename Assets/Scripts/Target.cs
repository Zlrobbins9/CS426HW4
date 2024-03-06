using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding namespaces
using Unity.Netcode;

public class Target : NetworkBehaviour
{
    public Score scoreManager;
    private void OnCollisionEnter(Collision collision)
    {
        // Check if this GameObject has any children
        if (transform.childCount > 0)
        {
            // Get the Renderer component of the 2nd child
            Renderer mouse = transform.GetChild(1).GetComponent<Renderer>();

            // Get the Renderer component of the colliding GameObject (Object B)
            Renderer target = collision.gameObject.GetComponent<Renderer>();

            // Make sure both Renderers are found
            if (mouse != null && target != null)
            {
                // Compare the materials
                if (mouse.material.name == target.material.name)
                {
                    Debug.Log("Materials match!");
                    ///scoreManager.AddPoint();
                    Debug.Log("Error: ScoreManager.AddPoint is broken, please fix and uncomment the line above");
                    DestroyTargetServerRpc();
                    // Add any additional logic here for when materials match
                }
                else
                {
                    Debug.Log("Materials do not match.");
                    // Add any additional logic here for when materials do not match
                }
            }
            else
            {
                // One or both of the Renderers were not found
                Debug.LogError("One or both objects do not have a Renderer component with a material.");
            }
        }
        else
        {
            // This GameObject does not have children
            Debug.LogError("This GameObject has no children to compare materials with.");
        }
    }



    // client can not spawn or destroy objects
    // so we need to use ServerRpc
    // we also need to add RequireOwnership = false
    // because we want to destroy the object even if the client is not the owner
    [ServerRpc(RequireOwnership = false)]
    public void DestroyTargetServerRpc()
    {
        //despawn
        GetComponent<NetworkObject>().Despawn(true);
        //after collision is detected destroy the gameobject
        Destroy(gameObject);
    }
}