using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
     
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PhotonView>().RPC("DestroyBullet",RpcTarget.AllBuffered,15.0f);
    }

    [PunRPC]
    void DestroyBullet(float time)
    {
        Destroy(gameObject,time);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!photonView.IsMine) return;

        PhotonView target = other.gameObject.GetComponent<PhotonView>();
        if(target && (!target.IsMine || target.IsSceneView))
        {
            GetComponent<PhotonView>().RPC("DestroyBullet",RpcTarget.AllBuffered,0f);
        }
    }
}
