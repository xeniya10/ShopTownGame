using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public static class AnimationUtility
{
    public static void Fade(Image image, int alpha, float time, Sequence sequence, Action callBack)
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

    public static void Fade(TextMeshProUGUI text, int alpha, float time, Sequence sequence, Action callBack)
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

    public static void Fill(Image image, TextMeshProUGUI text, float duration, Sequence sequence, Action callBack)
    {
        float currentTime = 0;

        if (sequence != null)
        {
            sequence.Append(image.DOFillAmount(0, duration)
            .OnUpdate(() => text.SetText(TimeToString(TimeSpan.FromSeconds(duration - (currentTime += Time.deltaTime)))))
            .OnComplete(() => callBack?.Invoke()));
        }

        else
        {
            image.DOFillAmount(0, duration)
                .OnUpdate(() => text.SetText(TimeToString(TimeSpan.FromSeconds(duration - (currentTime += Time.deltaTime)))))
                .OnComplete(() => callBack?.Invoke());
        }
    }

    public static string TimeToString(TimeSpan timeSpan)
    {
        if (timeSpan.Hours == 0 && timeSpan.Minutes == 0)
        {
            return timeSpan.ToString(timeSpan.Seconds < 10 ? @"s\.f" : @"ss\.f");
        }

        if (timeSpan.Hours == 0)
        {
            return timeSpan.ToString(timeSpan.Minutes < 10 ? @"m\:ss" : @"mm\:ss");
        }

        return timeSpan.ToString(timeSpan.Hours < 10 ? @"h\:mm\:ss" : @"hh\:mm\:ss");
    }

    public static void Scale(Transform objectTransform, Vector2 scale, float time, Sequence sequence, Action callBack)
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

    public static void Move(Transform objectTransform, Vector2 targetPosition, float time, Sequence sequence)
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

    public static void MoveFromScreenBorder(Transform objectTransform, float xFactor, float yFactor, float time, Sequence sequence)
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

    public static void MoveToScreenBorder(Transform objectTransform, float xFactor, float yFactor, float time, Sequence sequence)
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