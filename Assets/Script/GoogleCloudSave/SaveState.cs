using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.EncryptedSave;

public class SaveState : MonoBehaviour
{
}
[System.Serializable]
public class PlayerClass
{
    int finalScore = ES_Save.Load<int>("finalScore");
    float money = ES_Save.Load<float>("money");
    int selectedSkinNEW = ES_Save.Load<int>("selectedSkinNEW");
    //float allBoosterFasterTimeUp = ES_Save.Load<float>("allBoosterFasterTimeUp");
    //float allBoosterFasterTimeDown = ES_Save.Load<float>("allBoosterFasterTimeDown");
    bool[] allBoosterFasterBool = ES_Save.Load<bool[]>("allBoosterFasterBool");
   // float timeUbreakableBooster = ES_Save.Load<float>("timeUbreakableBooster");
    bool[] ubreakableBool = ES_Save.Load<bool[]>("ubreakableBool");
   // float timeScoreBooster = ES_Save.Load<float>("timeScoreBooster");
    //float jumpBoosterTime2 = ES_Save.Load<float>("jumpBoosterTime2");
    bool[] doubleScoreBooster = ES_Save.Load<bool[]>("doubleScoreBooster");
    bool[] jumpBoosterBool = ES_Save.Load<bool[]>("jumpBoosterBool");
    bool[] skinlistNEW = ES_Save.Load<bool[]>("skinlistNEW");
}

