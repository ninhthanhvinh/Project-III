using TMPro;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volumn = .7f;
    [Range(.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, .5f)]
    public float randomVolumn = .1f;
    [Range(0f, .5f)]
    public float randomPitch = .1f;

    [Range(0f, 1f)]
    public float spatialBlend = .5f;

    public bool loop = false;

    public bool isMusic;

    private AudioSource source;
    public AudioSource Source { get { return source; } }

    public void SetSource(AudioSource _source, Transform parent)
    {
        if (_source == null) return;
        if (_source.transform.Equals(parent)) return;
        source = _source;
        source.transform.SetParent(parent);
        source.transform.localPosition = Vector3.zero;
        source.clip = clip;
        source.loop = loop;
        source.spatialBlend = spatialBlend;
    }

    public void Play()
    {
        if (source == null) return;
        source.volume = volumn * (1 + Random.Range(-randomVolumn / 2f, randomVolumn / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    [SerializeField] private AudioMixer _musicMixer;
    [SerializeField] private AudioMixer _sfxMixer;

    public AudioMixer MusicMixer { get { return _musicMixer; } }
    public AudioMixer SFXMixer { get { return _sfxMixer; } }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 audio manager in a scene");
        }
        else
        {
            instance = this;
        }
        
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            AudioSource _source = _go.AddComponent<AudioSource>();
            if (sounds[i].isMusic)
            {
                _source.outputAudioMixerGroup = _musicMixer.FindMatchingGroups("Master")[0];
            }
            else
            {
                _source.outputAudioMixerGroup = _sfxMixer.FindMatchingGroups("Master")[0];            
            }

            sounds[i].SetSource(_source, transform);
        }

        PlaySound("riskbgm", this.transform);
    }

    public void PlaySound(string _name, Transform owner)
    {
        
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                //sounds[i].SetSource(sounds[i].Source, owner);
                sounds[i].SetSource(CreateSource(owner), owner);

                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("Audio Manager: Sound not found");
    }


    private AudioSource CreateSource(Transform parent)
    {
        var source = parent.GetComponentInChildren<AudioSource>();
        if (source != null)
        {
            return source;
        }

        GameObject _go = new("Sound_");
        _go.transform.SetParent(parent);
        source = _go.AddComponent<AudioSource>();
        return source;

    }
}