using UnityEngine;
using System.Collections;

public class PlayerBlink : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float blinkDuration = 1f;
    public int blinkCount = 5;

    public void GetHit()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        float blinkInterval = blinkDuration / (blinkCount * 2);

        for (int i = 0; i < blinkCount; i++)
        {
            // Fade out
            SetSpriteAlpha(0);
            yield return new WaitForSeconds(blinkInterval);

            // Fade in
            SetSpriteAlpha(1);
            yield return new WaitForSeconds(blinkInterval);
        }

        // Ensure the sprite is fully visible at the end
        SetSpriteAlpha(1);
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
