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
        [SerializeField, Header("看完廣告的鑽石"), Range(0, 300)] private int addDimondValue;
        [SerializeField, Header("看完廣告的能量"), Range(0, 500)] private int addEnergyValue;

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
            print("<color=green>廣告初始化成功</color>");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>廣告初始化失敗，原因:" + message + "</color>");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>廣告載入成功"+ placementId +"</color>");

        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>廣告載入失敗，原因:" + placementId + "</color>");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=green>廣告顯示失敗" + message + "</color>");        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>廣告顯示開始" + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>廣告顯示點擊" + placementId + "</color>");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>廣告顯示完成" + placementId + "</color>");
            dimondPlayer += addDimondValue;
            textDimond.text = dimondPlayer.ToString();
        }

        private void LoadAds()
        {
            print("載入廣告，ID:" + adsId);
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
