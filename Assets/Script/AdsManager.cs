using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEditor;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Rendering;

namespace remiel
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField, Header("�ݧ��s�i���p��"), Range(0, 300)] private int addDimondValue;
        [SerializeField, Header("�ݧ��s�i����q"), Range(0, 500)] private int addEnergyValue;

        private Text textDimond;

        private int dimondPlayer;
        private int energyPlayer;

        private Button btnDimond;

        private string gameIdAndroid = "4759579";
        private string gameIdIos = "4759578";
        private string gameId;

        private string adsIdAndroid = "AddDimond";
        private string adsIdIos = "AddDimond";
        private string adsId;                

        public void OnInitializationComplete()
        {
            print("<color=green>�s�i��l�Ʀ��\</color>");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>�s�i��l�ƥ��ѡA��]:" + message + "</color>");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>�s�i���J���\"+ placementId +"</color>");

        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>�s�i���J���ѡA��]:" + placementId + "</color>");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=green>�s�i��ܥ���" + message + "</color>");        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>�s�i��ܶ}�l" + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>�s�i����I��" + placementId + "</color>");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>�s�i��ܧ���" + placementId + "</color>");
            dimondPlayer += addDimondValue;
            textDimond.text = dimondPlayer.ToString();
        }

        private void LoadAds()
        {
            print("���J�s�i�AID:" + adsId);
            Advertisement.Load(adsId, this);
        }

        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

        private void Awake()
        {
            btnDimond = GameObject.Find("BtnDimond").GetComponent<Button>();
            btnEnergy = GameObject.Find("BtnENergy").GetComponent<Button>();
            btnDimond.onClick.AddListener(LoadAds);

            InitializeAds();

#if UNTIY_IOS
            adsId = adsIdAndroid;
#elif UNITY_ANDROID
            adsId = adsIdIos;
#endif

            // PC test
            adsId = adsIdAndroid;
        }

        private void InitializeAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }

    }
}
