using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public static class AnimationExtensions
{
    public static void Fade(this Image image, float alpha, float time,
        Sequence sequence, Action callBack)
    {
        if (sequence != null)
        {
            sequence.Append(image.DOFade(alpha, time)
                .OnComplete(() => callBack?.Invoke()));
        }

        else
        {
            image.DOFade(alpha, time)
                .OnComplete(() => callBack?.Invoke());
        }
    }

    public static void Fade(this TextMeshProUGUI text, float alpha, float time,
        Sequence sequence, Action callBack)
    {
        if (sequence != null)
        {
            sequence.Append(text.DOFade(alpha, time)
                .OnComplete(() => callBack?.Invoke()));
        }

        else
        {
            text.DOFade(alpha, time)
                .OnComplete(() => callBack?.Invoke());
        }
    }

    public static void Fill(this Image image, TextMeshProUGUI text, float duration,
        Action callBack)
    {
        float currentTime = 0;

        image.DOFillAmount(0, duration)
            .OnUpdate(() => text.SetText(TimeSpan.FromSeconds(duration - (currentTime += Time.deltaTime))
                .ToFormattedString()))
            .OnComplete(() => callBack?.Invoke());
    }

    public static void Scale(this Transform objectTransform, Vector2 scale, float time,
        Sequence sequence, Action callBack)
    {
        if (sequence != null)
        {
            sequence.Append(objectTransform.DOScale(scale, time)
                .OnComplete(() => callBack?.Invoke()));
        }

        else
        {
            objectTransform.DOScale(scale, time)
                .OnComplete(() => callBack?.Invoke());
        }
    }

    public static void Move(this Transform objectTransform, Vector2 targetPosition, float time,
        Sequence sequence)
    {
        if (sequence != null)
        {
            sequence.Append(objectTransform.DOLocalMove(targetPosition, time));
        }

        else
        {
            objectTransform.DOLocalMove(targetPosition, time);
        }
    }

    public static void MoveFromScreenBorder(this Transform objectTransform, float xFactor, float yFactor,
        float time, Sequence sequence)
    {
        var targetPosition = objectTransform.localPosition;

        var startX = targetPosition.x + xFactor * Screen.width;
        var startY = targetPosition.y + yFactor * Screen.height;
        var startPosition = new Vector2(startX, startY);

        objectTransform.localPosition = startPosition;

        if (sequence != null)
        {
            sequence.Append(objectTransform.DOLocalMove(targetPosition, time));
        }

        else
        {
            objectTransform.DOLocalMove(targetPosition, time);
        }
    }

    public static void MoveToScreenBorder(this Transform objectTransform, float xFactor, float yFactor,
        float time, Sequence sequence)
    {
        var startPosition = objectTransform.localPosition;

        var targetX = startPosition.x + xFactor * Screen.width;
        var targetY = startPosition.y + yFactor * Screen.height;
        var targetPosition = new Vector2(targetX, targetY);

        if (sequence != null)
        {
            sequence.Append(objectTransform.DOLocalMove(targetPosition, time));
        }

        else
        {
            objectTransform.DOLocalMove(targetPosition, time);
        }
    }
}
