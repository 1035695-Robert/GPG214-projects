using System.IO;
using UnityEngine;

public class LoadAudio : MonoBehaviour
{
    public string fileName = "ConveyorBelt.wav";
    public string folderName = "Audio";
    private string combinedFilePath;
    
    private AudioSource audioSource;
    private AudioClip beltClip;
    
    void Start()
    {
        combinedFilePath = Path.Combine(Application.streamingAssetsPath, folderName, fileName);
        audioSource = GetComponent<AudioSource>();
        
        if(audioSource == null )
        {
            Debug.Log("error no audio source component attachechded");
            return;
        }

        LoadSoundFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlaySound();
        }
    }
    void LoadSoundFromFile()
    {
        if (File.Exists(combinedFilePath))
        {
            //store the physical data in this array
            byte[] audioData = File.ReadAllBytes(combinedFilePath);

            //convert to float array (divided by 2 since each sample is represented by 2bits to a byte)
            float[] floatArray = new float[audioData.Length / 2];

            //we loop over the array
            for (int i = 0; i < floatArray.Length; i++)
            {
                //convert the audiodata to a 16bit int
                short bitValue = System.BitConverter.ToInt16(audioData, i * 2);
                //normalise the current value between -1,1 with the 32768 being the max value
                floatArray[i] = bitValue / 32768.0f;
            }
            //call the create function
            beltClip = AudioClip.Create("beltClip", floatArray.Length, 1, 44100, false);

            //set the audio data 
            beltClip.SetData(floatArray, 0);
        }
        else
        {
            Debug.Log("no File Found At Path " + combinedFilePath);
        }
    }
    void PlaySound()
    {
        if (audioSource == null || beltClip == null)
        {
            return;
        }
        audioSource.loop = true;
        audioSource.PlayOneShot(beltClip);
    }
}

