using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private Sprite _daySkin;
    [SerializeField] private Sprite _nightSkin;       
    
    public void DaySprite()
    {
        _renderer.sprite = _daySkin;
    }
    
    public void NightSprite()
    {
        _renderer.sprite = _nightSkin;
    }
}
