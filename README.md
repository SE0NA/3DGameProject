# 3D 지뢰 찾기
#### 기존 고전 게임인 2D 지뢰 찾기를 1인칭 3D로 재디자인하여 구현된 게임.
###### (본 프로젝트는 [sweeper3D](https://github.com/SE0NA/sweeper-3D)에서 재설계·보완·발전되었다.)

<img src="https://user-images.githubusercontent.com/85846475/205481453-b6e19094-794f-4612-baee-ee854bb6a61b.gif" width="300" height="230"><br>
<h6><a href="https://github.com/SE0NA/3DGameProject/blob/main/play_video.mp4">전체 시연 영상</a></h6><br>

***

1. 프로젝트 명: 3D 지뢰 찾기

2. (게임프로그래밍) 개인 텀프로젝트

3. 개발 기간: 약 2개월

***
### 소개 <br>

3D 지뢰 찾기는 기존 고전 게임인 2D 지뢰 찾기를 1인칭 3D로 재디자인하여 구현된 게임이다. <br>
기존 지뢰 찾기의 마우스 커서는 플레이어블 오브젝트, 단추는 각 방으로 대치되어 게임을 구성한다. <br>
스테이지의 방에는 랜덤으로 지뢰가 설치되어 있어, 플레이어는 주변 지뢰 수 정보를 이용해 지뢰를 제외한 모든 방을 탐색해야 한다. <br><br>

##### 1. 게임 시작 화면 <br>
<img src="https://user-images.githubusercontent.com/85846475/205479098-18794ff2-66c1-415a-b945-3e0e128a0a61.png" width="300" height="230">
<br>

##### 2. 게임 플레이 화면 <br>
<h6><div>
<img src="https://user-images.githubusercontent.com/85846475/205479168-3dd317f9-202c-4928-8c1f-507c973a85a3.png" width="280" height="200">
<img src="https://user-images.githubusercontent.com/85846475/205479265-d17986ef-abe3-4126-8ec9-f42c49b23c06.png" width="280" height="180">
<img src="https://user-images.githubusercontent.com/85846475/205479328-5d132e82-8c5b-4173-a22b-512269c7ad58.png" width="280" height="200"><br>
<img src="https://user-images.githubusercontent.com/85846475/205481555-6a7f79bb-2548-4817-9133-52bf59689dd3.png">
</div>
키보드의 w,a,s,d로 캐릭터를 이동하며, 캐릭터 오브젝트가 문과 가까이(BoxCollider 범위)에 위치할 때 문과 상호작용이 가능하도록 하였다.<br>
문과의 상호작용 내용은 마우스 왼쪽 클릭 또는 오른쪽 클릭으로 진행한다.</h6><br>

##### 3. 게임 엔딩 <br>
<h6><div>
<img src="https://user-images.githubusercontent.com/85846475/205479489-ce38e724-6a46-482d-8df6-634f5cbf8d0a.png" width="300" height="230">
<img src="https://user-images.githubusercontent.com/85846475/205479497-d31ad092-a619-45cf-8ad5-6e3a36c6b5a6.png" width="300" height="230">
</div>
지뢰가 있는 방을 제외한 모든 방을 열면 게임 클리어, 지뢰가 있는 방을 열면 게임 오버된다.</h6><br>

###### 3-1. DB 저장
<h6>
<div><img src="https://user-images.githubusercontent.com/85846475/205479565-baee5610-8b26-4c8a-9af1-9aa85a8ba41d.png"></div>
게임 클리어 화면에서 이름을 입력하면 게임 정보를 DB에 저장한다.(php).</h6><br>

<br><br>

***
### 메인 코드
<ul>
<li> <a href="https://github.com/SE0NA/3DGameProject/blob/main/TheDoor/Assets/Scripts/Game/GameManager.cs">GameManager.cs</a></li>
<li> <a href="https://github.com/SE0NA/3DGameProject/blob/main/TheDoor/Assets/Scripts/Game/PlayerController.cs">PlayerController.cs</a></li>
<li> <a href="https://github.com/SE0NA/3DGameProject/blob/main/TheDoor/Assets/Scripts/Game/StageInfo.cs">StageInfo.cs</a></li>
<li> <a href="https://github.com/SE0NA/3DGameProject/blob/main/TheDoor/Assets/Scripts/Game/RoomInfo.cs">RoomInfo.cs</a></li>
</ui>

***
#### 사용 에셋
<h6><table>
<tr><td>플레이어 오브젝트</td><td>Robot Solider - Marcelo Barrio</td></tr>
<tr><td>스테이지 구성</td><td>3D Free Modular Kit – Barking Dog</td></tr>
<tr><td>음향 효과</td><td><p>Ash Valley Cybernetics LITE – Neal Bond <br>8 Bits Elements – Game Sound Solutions</p></td></tr>
<tr><td>애니메이션</td><td><p>Melee Warrior Animations FREE – Kevin Iglesias <br>Standard Assets(for Unity 2018.4) - Unity Technologies</p></td></tr>
</table></h6>
