using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    [SerializeField] private GameObject _rewardItem;
    private Animator _anim;
    private bool _playState;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _playState = false;
    }

    public void Play()
    {
        if (!_playState)
        {
            _playState = true;
            StartCoroutine(PlayCoroutine());
        }

        

    }

    private IEnumerator PlayCoroutine()
    {
        _anim.SetBool("Handle", true);
        yield return new WaitForSeconds(1);
        _anim.SetBool("Handle", false);
        yield return new WaitForSeconds(1);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);
        Instantiate(_rewardItem,pos,Quaternion.identity);
        
        _playState = false;
    }

    public bool GetPlayState()
    {
        return _playState;
    }
}
