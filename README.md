# 🛡️ Kingdom Wars: Isometric Defense

**Kingdom Wars** là một trò chơi chiến thuật thủ thành (Tower Defense) góc nhìn Isometric 2.5D, lấy bối cảnh cuộc giao tranh khốc liệt giữa các vương quốc thời Trung Cổ. Người chơi đóng vai trò một đại tướng quân, xây dựng hệ thống phòng thủ kiên cố để bảo vệ kinh thành trước các đợt tấn công của quân đoàn lân bang.

---

## 🏰 Tổng quan trò chơi
Trong thế giới của Kingdom Wars, mỗi vùng đất có những loại quân đội đặc trưng. Người chơi phải tính toán vị trí đặt tháp canh (Archers, Mage Towers, Barracks) để tối ưu hóa sát thương và ngăn chặn kẻ địch tiến vào cổng thành.

### Các tính năng chính:
* **Isometric Grid System:** Hệ thống lưới chuẩn xác cho phép người chơi đặt tháp canh trên các ô đất (Tiles) cố định.
* **Hệ thống quân địch đa dạng (Wave System):** Kẻ địch tấn công theo từng đợt với độ khó tăng dần, bao gồm lính bộ binh, kỵ binh và trùm (Boss) cuối mỗi màn.
* **Hệ thống tháp canh:** Có nhiều loại tháp canh với nhiều cách chơi và các hiệu ứng riêng biệt.
* **AI Pathfinding:** Quân địch tự động tìm đường ngắn nhất từ điểm xuất phát đến kinh thành, né tránh các chướng ngại vật.

---

## 🛠 Kỹ thuật & Giải pháp lập trình (Technical Highlights)
Dự án thể hiện khả năng quản lý logic phức tạp và tối ưu hóa hệ thống chiến thuật:

1.  **A* Pathfinding Algorithm:** Tối ưu hóa thuật toán tìm đường cho quân lính di chuyển mượt mà trên bản đồ Isometric.
2.  **ScriptableObjects Architecture:** Quản lý toàn bộ dữ liệu về tháp canh (Giá tiền, Sát thương, Tốc độ bắn) và các loại quân địch, giúp dễ dàng cân bằng game (Rebalancing) mà không cần chỉnh sửa code.
3.  **Object Pooling:** Áp dụng cho hệ thống đạn dược (Projectiles) và hiệu ứng nổ (VFX), đảm bảo FPS ổn định ngay cả khi hàng trăm mũi tên bay cùng lúc.
4.  **Isometric Sorting Order:** Xử lý lớp hiển thị (Layering) chuyên sâu để đảm bảo các nhân vật và công trình được vẽ đúng thứ tự chiều sâu, không bị đè lấp sai thực tế.
5.  **State Machine:** Quản lý vòng đời của tháp (Tìm mục tiêu -> Nhắm bắn -> Hồi chiêu) và trạng thái quân lính (Di chuyển -> Bị tấn công -> Chết).

---

## 🕹 Hướng dẫn chơi
* **Chọn tháp:** Nhấp chuột vào biểu tượng tháp ở thanh công cụ phía dưới.
* **Đặt tháp:** Nhấp chuột vào các ô trống trên bản đồ để xây dựng.
* **Nâng cấp/Bán:** Nhấp vào tháp đã xây để mở Menu tương tác.
* **Kích hoạt kỹ năng:** Nhấn phím `1`, `2`, `3` để sử dụng các phép bổ trợ.
