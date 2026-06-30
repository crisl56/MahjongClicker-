using System;
using BreakInfinity;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private string[] suffixes = {
        "",                                          
        "K", "M", "B", "T",                         // 10^3  - 10^12
        "Qa", "Qi", "Sx", "Sp", "Oc", "No",         // 10^15 - 10^30
        "Dc", "Ud", "Dd", "Td", "Qad", "Qid",       // 10^33 - 10^48
        "Sxd", "Spd", "Ocd", "Nod",                 // 10^51 - 10^60
        "Vg", "Uvg", "Dvg", "Tvg", "Qavg", "Qivg", // 10^63 - 10^78
        "Sxvg", "Spvg", "Ocvg", "Novg",             // 10^81 - 10^90
        "Tg", "Utg", "Dtg", "Ttg", "Qatg", "Qitg", // 10^93 - 10^108
        "Sxtg", "Sptg", "Octg", "Notg",             // 10^111 - 10^120
        "Qag", "Uqag", "Dqag", "Tqag", "Qaqag",    // 10^123 - 10^135
        "Qiqag", "Sxqag", "Spqag", "Ocqag", "Noqag",// 10^138 - 10^150
        "Qig", "Uqig", "Dqig", "Tqig", "Qaqig",    // 10^153 - 10^165
        "Qiqig", "Sxqig", "Spqig", "Ocqig", "Noqig",// 10^168 - 10^180
        "Sxg", "Usxg", "Dsxg", "Tsxg", "Qasxg",    // 10^183 - 10^195
        "Qisxg", "Sxsxg", "Spsxg", "Ocsxg", "Nosxg",// 10^198 - 10^210
        "Spg", "Uspg", "Dspg", "Tspg", "Qaspg",    // 10^213 - 10^225
        "Qispg", "Sxspg", "Spspg", "Ocspg", "Nospg",// 10^228 - 10^240
        "Ocg", "Uocg", "Docg", "Tocg", "Qaocg",    // 10^243 - 10^255
        "Qiocg", "Sxocg", "Spocg", "Ococg", "Noocg",// 10^258 - 10^270
        "Nog", "Unog", "Dnog", "Tnog", "Qanog",     // 10^273 - 10^285
        "Qinog", "Sxnog", "Spnog", "Ocnog", "Nonog",// 10^288 - 10^300
        "Ct"                                         // 10^303 (Centillion)
    };

    public void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        CoinManager.Instance.CoinsUpdated.AddListener(UpdateCoinScore);

        UpdateCoinScore();
    }

    public void OnDestroy()
    {
        CoinManager.Instance.CoinsUpdated.RemoveListener(UpdateCoinScore);
    }

    void UpdateCoinScore()
    {
        scoreText.text = Format(CoinManager.Instance.Coins);
    }

    public string Format(BigDouble value, int decimals = 1)
    {
        if (value < 1000)
            return value.ToString("F" + 0 );

        int suffixIndex = (int)(BigDouble.Log10(value) / 3);

        if (suffixIndex < suffixes.Length)
        {
            BigDouble divided = value / BigDouble.Pow(1000, suffixIndex);
            return divided.ToString("F" + decimals) + suffixes[suffixIndex];
        }

        // Beyond suffix table: fall back to scientific notation
        return value.ToString("E" + decimals);
    }
}