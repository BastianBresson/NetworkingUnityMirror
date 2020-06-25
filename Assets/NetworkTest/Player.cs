using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{

    [SerializeField] private Vector3 movement = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!hasAuthority) { return; }

        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        CmdMove();
    }

    [Command]
    private void CmdMove()
    {
        // validate here

        RpcMove();
    }

    [ClientRpc]
    private void RpcMove()
    {
        transform.Translate(movement);
    }
}


