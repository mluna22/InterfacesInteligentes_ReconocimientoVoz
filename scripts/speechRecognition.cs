using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using HuggingFace.API;
using TMPro;

public class speechRecognition : MonoBehaviour
{
    public delegate void message(string text);
    public event message OnSpeechRecognized;
    private AudioClip clip;
    private byte[] bytes;
    private bool recording;
    public TextMeshProUGUI text;

    private void Update() {
        if (recording && (Microphone.GetPosition(null) >= clip.samples || !Microphone.IsRecording(null)) ) {
            StopRecording();
        }
    }

    public void StartRecording() {
        Debug.Log("Recording...");
        text.text = "Recording...";
        clip = Microphone.Start(null, false, 3, 44100);
        recording = true;
    }

    public void StopRecording() {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
    }

    private void SendRecording() {
        Debug.Log("Sending...");
        text.text = "Sending...";
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            OnSpeechRecognized?.Invoke(response);
            text.text = response;
            Debug.Log(response);
        }, error => {
            Debug.Log(error);
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
            using (var writer = new BinaryWriter(memoryStream)) {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples) {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }
    }