using UnityEngine.UI; //for accessing Sliders and Dropdown
using System.Collections.Generic; // So we can use List<>
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{

	public float s_MicLoudness1;
	public float minThreshold = 0;
	public float frequency = 0.0f;
	public int audioSampleRate = 4400;
	private int m_sampleWindow = 128;
	public string microphone;
	private AudioClip m_clipRecord1 = null;
	public FFTWindow fftWindow;
	public Dropdown micDropdown;
	public Slider thresholdSlider;

	public bool flapped;

	public Slider m_volumeMicrophone;

	private List<string> options = new List<string>();
	private int samples = 8192;

	private float timer = 0; 

	private static MicrophoneInput s_Instance;

	public static MicrophoneInput getInstance()
	{
		if (s_Instance == null)
		{
			s_Instance = GameObject.FindObjectOfType<MicrophoneInput>();
		}
		return s_Instance;
	}

	void Start()
	{
		// get all available microphones
		foreach (string device in Microphone.devices)
		{
			if (microphone == null)
			{
				//set default mic to first mic found.
				microphone = device;
			}
			options.Add(device);
		}
		microphone = options[PlayerPrefsManager.GetMicrophone()];
		minThreshold = PlayerPrefsManager.GetThreshold();

		//add mics to dropdown
		micDropdown.AddOptions(options);
		micDropdown.onValueChanged.AddListener(delegate {
			micDropdownValueChangedHandler(micDropdown);
		});

		thresholdSlider.onValueChanged.AddListener(delegate {
			thresholdValueChangedHandler(thresholdSlider);
		});

		m_clipRecord1 = Microphone.Start(microphone, true, 1, audioSampleRate);
	}

	private void Update()
	{
		s_MicLoudness1 = LevelMax(microphone, m_clipRecord1);
		if (s_MicLoudness1 > minThreshold)
		{
			m_volumeMicrophone.value = s_MicLoudness1;
		}
		if (s_MicLoudness1 < minThreshold) {
			m_volumeMicrophone.value = 0;
		}
	}

	float LevelMax(string _device, AudioClip _clipRecord)
	{
		float levelMax1 = 0;
		float[] waveData1 = new float[m_sampleWindow];
		if (!(Microphone.GetPosition(_device) > 0))
		{
			return 0;
		}
		int micPosition1 = Microphone.GetPosition(_device) - (m_sampleWindow + 1);
		if (micPosition1 < 0)
		{
			return 0;
		}
		else
		{
			_clipRecord.GetData(waveData1, micPosition1);
			for (int i = 0; i < m_sampleWindow; ++i)
			{
				float wavePeak1 = waveData1[i] * waveData1[i];
				if (levelMax1 < wavePeak1)
				{
					levelMax1 = wavePeak1;
				}

			}
		}
		return levelMax1*100;
	}


	public void micDropdownValueChangedHandler(Dropdown mic)
	{
		microphone = options[mic.value];
	}

	public void thresholdValueChangedHandler(Slider thresholdSlider)
	{
		minThreshold = thresholdSlider.value;
	}

	/*public float GetAveragedVolume()
	{
		float[] data = new float[256];
		float a = 0;
		m_clipRecord1.GetOutputData(data, 0);
		foreach (float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a / 256;
	}

	public float GetFundamentalFrequency()
	{
		float fundamentalFrequency = 0.0f;
		float[] data = new float[samples];
		m_clipRecord1.GetSpectrumData(data, 0, fftWindow);
		float s = 0.0f;
		int i = 0;
		for (int j = 1; j < samples; j++)
		{
			if (data[j] > minThreshold) // volumn must meet minimum threshold
			{
				if (s < data[j])
				{
					s = data[j];
					i = j;
				}
			}
		}
		fundamentalFrequency = i * audioSampleRate / samples;
		frequency = fundamentalFrequency;
		return fundamentalFrequency;
	}
	*/
}
