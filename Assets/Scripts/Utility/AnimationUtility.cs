using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public static class AnimationUtility
{
    public static void Fade(Image image, int alpha, float time, Sequence sequence, Action CallBack)
    {
        if (sequence != null)
        {
            sequence.Append(image.DOFade(alpha, time)
            .OnComplete(() => CallBack?.Invoke()));
        }

        else
        {
            var animation = image.DOFade(alpha, time)
            .OnComplete(() => CallBack?.Invoke());
        }
    }

    public static void Fade(TextMeshProUGUI text, int alpha, float time, Sequence sequence, Action CallBack)
    {
        if (sequence != null)
        {
            sequence.Append(text.DOFade(alpha, time)
            .OnComplete(() => CallBack?.Invoke()));
        }

        else
        {
            var animation = text.DOFade(alpha, time)
            .OnComplete(() => CallBack?.Invoke());
        }
    }

    public static void Fill(Image image, TextMeshProUGUI text, float duration, Sequence sequence, Action CallBack)
    {
        float currentTime = 0;

        if (sequence != null)
        {
            sequence.Append(image.DOFillAmount(0, duration)
            .OnUpdate(() => text.SetText(TimeToString(TimeSpan.FromSeconds(duration - (currentTime += Time.deltaTime)))))
            .OnComplete(() => CallBack?.Invoke()));
        }

        else
        {
            var animation = image.DOFillAmount(0, duration)
            .OnUpdate(() => text.SetText(TimeToString(TimeSpan.FromSeconds(duration - (currentTime += Time.deltaTime)))))
            .OnComplete(() => CallBack?.Invoke());
        }
    }

    public static string TimeToString(TimeSpan timeSpan)
    {
        if (timeSpan.Hours == 0 && timeSpan.Minutes == 0)
        {
            if (timeSpan.Seconds < 10)
            {
                return timeSpan.ToString(@"s\.f");
            }
            return timeSpan.ToString(@"ss\.f");
        }

        if (timeSpan.Hours == 0)
        {
            if (timeSpan.Minutes < 10)
            {
                return timeSpan.ToString(@"m\:ss");
            }
            return timeSpan.ToString(@"mm\:ss");
        }

        if (timeSpan.Hours < 10)
        {
            return timeSpan.ToString(@"h\:mm\:ss");
        }
        return timeSpan.ToString(@"hh\:mm\:ss");
    }

    public static void Scale(Transform objectTransform, Vector2 scale, float time, Sequence sequence, Action CallBack)
    {
        if (sequence != null)
        {
            sequence.Append(objectTransform.DOScale(scale, time)
            .OnComplete(() => CallBack?.Invoke()));
        }

        else
        {
            var animation = objectTransform.DOScale(scale, time)
            .OnComplete(() => CallBack?.Invoke());
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
            var moveAnimation = objectTransform.DOLocalMove(targetPosition, time);
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
            sequence?.Append(objectTransform.DOLocalMove(targetPosition, time));
        }

        else
        {
            var moveAnimation = objectTransform.DOLocalMove(targetPosition, time);
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
            sequence?.Append(objectTransform.DOLocalMove(targetPosition, time));
        }

        else
        {
            var moveAnimation = objectTransform.DOLocalMove(targetPosition, time);
        }
    }
}