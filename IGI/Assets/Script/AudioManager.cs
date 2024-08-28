using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager instance;
    public AudioSource audioSource;
    [Header("Customer")]
    public AudioClip orderSuccess;
    public AudioClip orderFailed;
    [Header("Machines")]
    public AudioClip craftMachine;
    public AudioClip destroyMachine;
    public AudioClip resupplyMachine;
    public AudioClip upgradeMachine;
    [Header("Monitor")]
    public AudioClip buyResource;
    [Header("Player")]
    public AudioClip swing;
    public AudioClip walk;
    [Header("Supply")]
    public AudioClip takeResource;
    [Header("Others")]
    public AudioClip[] buttonClick;
    public AudioClip[] changePhase;
    public AudioClip victory;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
