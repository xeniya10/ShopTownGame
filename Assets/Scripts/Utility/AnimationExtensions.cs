using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class AnimationExtensions
{
    public static void Fade(this Graphic image, float alpha, float time, Sequence sequence = null,
        Action callBack = null)
    {
        if (sequence != null)
        {
            sequence.Append(image.DOFade(alpha, time).OnComplete(() => callBack?.Invoke()));
        }

        else
        {
            image.DOFade(alpha, time).OnComplete(() => callBack?.Invoke());
        }
    }

    public static void Fill(this Image image, TextMeshProUGUI text, TimeSpan startTime, TimeSpan durationOfAnimation,
        Sequence sequence, Action callBack)
    {
        var currentTime = (float)startTime.TotalSeconds;
        var duration = (float)durationOfAnimation.TotalSeconds;

        sequence.Append(image.DOFillAmount(0, duration - currentTime)
            .OnUpdate(() =>
                text.SetText(TimeSpan.FromSeconds(duration - (currentTime += Time.deltaTime)).ToNumberFormatString()))
            .OnComplete(() => callBack?.Invoke()));
    }

    public static void Scale(this Transform objectTransform, Vector2 scale, float time, Sequence sequence = null,
        Action callBack = null)
    {
        if (sequence != null)
        {
            sequence.Append(objectTransform.DOScale(scale, time).OnComplete(() => callBack?.Invoke()));
        }

        else
        {
            objectTransform.DOScale(scale, time).OnComplete(() => callBack?.Invoke());
        }
    }

    public static void MoveFromScreenBorder(this Transform objectTransform, float xFactor, float yFactor, float time,
        Sequence sequence = null)
    {
        var targetPosition = objectTransform.localPosition;

        var startX = targetPosition.x + xFactor * Screen.width;
        var startY = targetPosition.y + yFactor * Screen.height;
        var startPosition = new Vector2(startX, startY);

        objectTransform.localPosition = startPosition;
        objectTransform.Move(targetPosition, time, sequence);
    }

    public static void MoveToScreenBorder(this Transform objectTransform, float xFactor, float yFactor, float time,
        Sequence sequence = null)
    {
        var startPosition = objectTransform.localPosition;

        var targetX = startPosition.x + xFactor * Screen.width;
        var targetY = startPosition.y + yFactor * Screen.height;
        var targetPosition = new Vector2(targetX, targetY);

        objectTransform.Move(targetPosition, time, sequence);
    }

    public static void Move(this Transform objectTransform, Vector2 targetPosition, float time,
        Sequence sequence = null)
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
}
