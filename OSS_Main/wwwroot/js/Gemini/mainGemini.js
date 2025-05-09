﻿
// Khi trang web load xong, hiển thị lời chào trong khu vực trả lời
window.addEventListener("DOMContentLoaded", () => {
    appendMessage("Fruitables AI", "Bạn có điều gì cần hỏi, hãy hỏi Fruitable AI của chúng tôi", "bg-light");
});


document.getElementById("askBtn").addEventListener("click", async () => {
    const userPrompt = document.getElementById("userPrompt").value.trim();
    const chatContainer = document.getElementById("chatContainer");

    if (!userPrompt) {
        alert("Please enter a question.");
        return;
    }

    // Hiển thị câu hỏi
    appendMessage("You", userPrompt, "bg-primary text-white");

    document.getElementById("userPrompt").value = "";

    // Loading message
    const loadingMsg = appendMessage("Fruitables AI", "Thinking...", "text-muted");

    try {
        const response = await fetch("/api/gemini/ask", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userPrompt)
        });

        const result = await response.text();

        // Xóa loading và thêm câu trả lời
        chatContainer.removeChild(loadingMsg);
        appendMessage("Fruitables AI", result || "No response received.", "bg-light");

    } catch (error) {
        chatContainer.removeChild(loadingMsg);
        appendMessage("Fruitables AI", `Error: ${error.message}`, "bg-danger text-white");
    }
});

function appendMessage(sender, message, styleClass = "") {
    const chatContainer = document.getElementById("chatContainer");

    const messageBlock = document.createElement("div");
    messageBlock.className = `mb-2 p-3 rounded ${styleClass}`;

    // Xử lý markdown cơ bản:
    const formatted = formatMessage(message);

    messageBlock.innerHTML = `<strong>${sender}:</strong><br>${formatted}`;
    chatContainer.appendChild(messageBlock);
    chatContainer.scrollTop = chatContainer.scrollHeight;

    return messageBlock;
}

function formatMessage(raw) {
    // Chuyển **bold** thành <strong>
    let html = raw.replace(/\*\*(.*?)\*\*/g, "<strong>$1</strong>");

    // Chuyển link thành <a>
    html = html.replace(/(https?:\/\/[^\s\)\]]+)/g, '<a href="$1" target="_blank">$1</a>');

    // Thay newline thành <br>
    html = html.replace(/\n/g, "<br>");

    return html;
}

// Cho phép nhấn Enter để gửi câu hỏi
document.getElementById("userPrompt").addEventListener("keydown", (event) => {
    if (event.key === "Enter") {
        event.preventDefault(); // Ngăn không cho xuống dòng
        document.getElementById("askBtn").click(); // Kích hoạt nút Ask
    }
});
