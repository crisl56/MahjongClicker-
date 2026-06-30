using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Image), typeof(Button))]
public class MahjongTileClicker : MonoBehaviour
{
    private Image _clickerImage;
    private Button _button;

    [SerializeField] private Sprite[] m_Sprites;
    private Queue<Sprite> m_SpritesQueue;

    public void Start()
    {
        _clickerImage = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void ResetQueue()
    {
        m_Sprites = m_Sprites.OrderBy(i => Random.value).ToArray();
        m_SpritesQueue = new Queue<Sprite>(m_Sprites);
    }

    private void OnClick()
    {
        if (m_SpritesQueue == null || m_SpritesQueue.Count > 0)
            ResetQueue();

        _clickerImage.sprite = m_SpritesQueue.Dequeue();

    }
}
