# unity-runningball-3dgame
러너 게임 제작

https://user-images.githubusercontent.com/90877724/162559147-927a9321-41cc-44b6-a508-ab4ff57c3cd5.mp4


## 1. 프로젝트 개요
### 1-1. 개발 인원/기간 및 포지션
- 1인, 총 2일 소요
### 개발 환경
- Unity 2020.3.16f
- 언어 : C#
- OS : Window 10

## 2. 핵심 구현 내용
### 2-1. [터치-드래그를 이용한 플레이어 이동](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/Movement.cs)
- X 좌표 이동(좌/우) 코루틴 함수 구현
```c#
private IEnumerator OnMoveToX(int direction)
    {
        float current = 0;
        float percent = 0;
        float start = transform.position.x;
        float end = transform.position.x + direction * moveXWidth;

        isXMove = true;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / moveTimeX;

            float x = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
        }

        isXMove = false;
    }
```
- Y 좌표 이동(포물선 점프) 코루틴 함수 구현
```c#
private IEnumerator OnMoveToY()
    {
        float current = 0;
        float percent = 0;
        float v0 = -gravity; // Y방향의 초기 속도

        isJump = true;
        rigidbody.useGravity = false;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeY;

            float y = originY + (v0 * percent) + (gravity * percent * percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield return null;
        }

        isJump = false;
        rigidbody.useGravity = true;
    }
```
- [마우스 클릭 제어](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/PlayerController.cs)
- [OnDragXY()](https://github.com/94mark/unity-runningball-3dgame/blob/80cb27f69e687936867c765a1a48a98e29b5a8e6/runningball/Assets/Scripts/PlayerController.cs#L63) 드래그 이동 함수 구현
- X 이동거리 = Mthf.Abs(touchEnd.x - touchStart.x), x 이동거리가 dragDistance(50)보다 크면 x축 이동
- Y 이동거리 = touchEnd.y - touchStart.y, y이동거리가 dragDistance(50)보다 크면 점프

### 2-2. [카메라 액션](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/CameraController.cs)
- 플레이어의 x, y 좌표값에는 영향을 받지 않고, z position과 차이를 유지하며 일정하게 플레이어 이동 추적

### 2-3. [구역 생성](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/AreaSpawner.cs)
- spawnAreaCountAtStart 개수만큼 area가 연달아 random 생성
- [지나간 구역은 삭제하고 새로운 구역 생성](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/Area.cs)

### 2-4 [코인 획득](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/Coin.cs) 및 UI
- 코인 획득 시 이펙트 생성 및 코인 파괴, OnTriggerEnter() 함수 사용
- [획득 코인 정보 출력 및 갱신](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/GameController.cs)

### 게임 시작 전 UI 설정
- [TapToPlay](https://github.com/94mark/unity-runningball-3dgame/blob/main/runningball/Assets/Scripts/FadeEffect.cs) 텍스트의 blink 코루틴 생성, 시작/종료 코루틴 반복 실행
```c#
 private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;
            color.a = Mathf.Lerp(start, end, percent);
            textFade.color = color;

            yield return null;
        }
    }
```
