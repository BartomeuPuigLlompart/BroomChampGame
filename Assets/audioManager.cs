using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour {

    public AudioSource audioSource;

    private AudioClip mainMenu;
    private AudioClip gameplay;

    public static audioManager AudioManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (AudioManager == null) AudioManager = this;
        else Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        mainMenu = Resources.Load<AudioClip>("Music/music/menu_music");
        gameplay = Resources.Load<AudioClip>("Music/music/In_Game");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnLevelWasLoaded(int level)
    {
        audioSource.Stop();
        if (level == 0)
            audioSource.clip = mainMenu;
        else
            audioSource.clip = gameplay;
        audioSource.Play();
    }
}
