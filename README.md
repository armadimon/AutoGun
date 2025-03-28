# AutoGun (임시)

## 개요
방치형 RPG를 만들어보는 개인 과제입니다.

---

## 주요 기능

### 자동 전투 시스템
- **자동 진행:** 플레이어가 별도의 조작 없이도 전투가 자동으로 진행되며, 캐릭터는 적을 탐색하고 전투를 수행합니다.
- **전투 전략:** 플레이어는 상황에 맞는 무기를 선택하고 각기 다른 무기 스킬을 이용하여 전투에 간접적으로 참여 할 수 있습니다.

### 인벤토리 시스템
- **동적 아이템 추가:** 게임 플레이 중 획득한 아이템을 인벤토리에 실시간으로 추가할 수 있습니다.
- **아이템 관리:** 장비 아이템과 소비 아이템을 구분하여 관리하며, UI를 통해 쉽게 확인 및 선택할 수 있습니다.
- **장비 및 소비:** 선택한 아이템에 따라 장착하거나 사용하여 게임 플레이에 즉각적인 영향을 미칠 수 있습니다.

### 장비 강화 시스템
- **장비 강화:** 골드를 소모하여 장비를 강화하며, 강화 수치에 따라 damage의 연산에서 배율값에 영향을 줍니다. 장비 레벨이 오르면 스킬이 해금되는 구조입니다.

---

## 설계 고려사항

  
- **데이터 관리:**  
  - ScriptableObject를 사용하여 아이템 및 장비, 스테이지 데이터와 스킬에 대한 기본 데이터를 관리하였습니다.

---

개선이 필요한 부분은 지속적으로 업데이트할 예정입니다!

---
