using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using Unity.VisualScripting;

public class MM_EffectPlayer : MonoBehaviour
{
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    public ParticleSystem _particle;
    // �G�t�F�N�g���o���ꏊ
    protected Transform _particleTransform;

    virtual public void Play()
    {
        CallParticleInstantiateSetCanceltoken();
    }
    virtual public void Play(float time)
    {
        CallParticleInstantiateSetCanceltoken(time);
    }

    virtual protected void CallParticleInstantiateSetCanceltoken()
    {
        
        var destroyCt = this.GetCancellationTokenOnDestroy();
        ParticleInstantiate(_particleTransform, destroyCt);

    }
    virtual protected void CallParticleInstantiateSetCanceltoken(float time)
    {
        var destroyCt = this.GetCancellationTokenOnDestroy();
        ParticleInstantiate(_particleTransform,time, destroyCt);
        
    }

    virtual async protected void ParticleInstantiate(Transform tf,CancellationToken token)
    {
        ParticleSystem particle = Instantiate(_particle, tf);

        float lifetime = particle.main.startLifetimeMultiplier;
        particle.Play();

        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifetime), cancellationToken: token);

        }
        catch (OperationCanceledException)
        {
            Debug.LogError($"�A�^�b�`����Ă���I�u�W�F�N�g��Destroy����܂���");
            return;
        }

        if (token.IsCancellationRequested)
        {
            return;
        }
        //Destroy(particle.gameObject);

    }
    virtual async protected void ParticleInstantiate(Transform tf, float time,CancellationToken token)
    {
        ParticleSystem particle = Instantiate(_particle, tf);

        float lifetime = time;

        particle.Play();

        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifetime), cancellationToken: token);

        }
        catch (OperationCanceledException)
        {
            Debug.LogError($"�A�^�b�`����Ă���I�u�W�F�N�g��Destroy����܂���");
            return;
        }
        if (token.IsCancellationRequested)
        {
            return;
        }
        Destroy(particle);

    }

    public void SetParticleTransform(Transform tf)
    {
        _particleTransform = tf;
    }
   
    public void SameParentObjectRotation()
    {
        _particleTransform.rotation = this.transform.rotation;
    }

    public void SameParentObjectScale()
    {
        _particleTransform.localScale = this.transform.localScale;
    }
}



