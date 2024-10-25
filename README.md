# Learn-DDD
https://dometrain.com/course/getting-started-domain-driven-design-ddd/

## Invariants 领域不变量
* **Session 课程**  
  (1) 每次课程的参与人数不能超过最大参与人数  
  (2) 在课程开始前 24 小时内，不能免费取消预约


* **Gym 健身房**  
  (1) 健身房的房间数量不能超过订阅允许的数量


* **Room 房间**  
  (1) 一个房间的课程数不能超过订阅允许的课程数
  (2) 一个房间不能有两个或多个重叠的课程


* **Subscription 订阅**  
  (1) 一个订阅不能拥有超过允许上限的健身房数量


* **Trainer 教练**  
  (1) 教练不能教两个或多个重叠的课程


* **Participant 参与者**  
  (1) 参与者不能保留重叠的课程