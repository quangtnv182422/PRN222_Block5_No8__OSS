/* Feedback Item Container */
.feedback - item {
    border: 1px solid #e9ecef; /* Subtle border for separation */
    border - radius: 0.5rem; /* Rounded corners */
    padding: 1rem; /* Inner spacing */
    margin - bottom: 1.5rem; /* Space between feedback items */
    background - color: #fff; /* White background */
    box - shadow: 0 1px 3px rgba(0, 0, 0, 0.1); /* Light shadow for depth */
}

/* Feedback Header (Avatar + User Info) */
.feedback - header {
    margin - bottom: 0.75rem; /* Space below header */
}

.feedback - avatar {
    width: 50px; /* Fixed size for avatar */
    height: 50px;
    border - radius: 50 %; /* Circular avatar */
    object - fit: cover; /* Ensure image fits well */
    border: 1px solid #dee2e6; /* Subtle border */
}

.feedback - user - info {
    display: flex;
    flex - direction: column;
    gap: 0.25rem; /* Space between username, email, and date */
}

.feedback - username {
    font - weight: 600; /* Bold username */
    font - size: 1rem;
}

.feedback - email,
.feedback - date {
    font - size: 0.875rem; /* Smaller text for email and date */
}

/* Feedback Body (Rating + Comment) */
.feedback - body {
    margin - bottom: 0.75rem; /* Space below body */
}

.feedback - rating {
    margin - bottom: 0.5rem; /* Space below stars */
}

.feedback - rating.fa - star {
    font - size: 1rem; /* Star size */
}

.feedback - comment {
    margin: 0; /* Remove default margin */
    font - size: 0.95rem; /* Slightly smaller comment text */
    line - height: 1.5; /* Better readability */
}

/* Feedback Actions (Edit + Delete Buttons) */
.feedback - actions {
    justify - content: flex - end; /* Align buttons to the right */
}

.feedback - form {
    display: inline - flex; /* Keep form inline with button */
    align - items: center;
}

/* Button Styling */
.feedback - actions.btn {
    padding: 0.375rem 1rem; /* Slightly larger padding */
    font - size: 0.875rem; /* Match Bootstrap btn-sm */
    border - radius: 0.25rem; /* Rounded corners */
    min - width: 80px; /* Consistent width */
    text - align: center;
    transition: all 0.2s ease; /* Smooth hover/focus transitions */
}

/* Primary Button (Edit) */
.feedback - actions.btn - outline - primary {
    border - color: #007bff; /* Bootstrap primary */
    color: #007bff;
}

.feedback - actions.btn - outline - primary: hover,
.feedback - actions.btn - outline - primary:focus {
    background - color: #007bff;
    color: #fff;
    border - color: #007bff;
}

/* Danger Button (Delete) */
.feedback - actions.btn - outline - danger {
    border - color: #dc3545; /* Bootstrap danger */
    color: #dc3545;
}

.feedback - actions.btn - outline - danger: hover,
.feedback - actions.btn - outline - danger:focus {
    background - color: #dc3545;
    color: #fff;
    border - color: #dc3545;
}

/* Accessibility: Focus Outline */
.feedback - actions.btn:focus {
    outline: 2px solid #007bff;
    outline - offset: 2px;
}

.feedback - actions.btn - outline - danger:focus {
    outline: 2px solid #dc3545;
}

/* Responsive Adjustments */
@media(max - width: 576px) {
    .feedback - item {
        padding: 0.75rem; /* Reduce padding on mobile */
    }

    .feedback - header {
        flex - direction: column; /* Stack avatar and info */
        align - items: flex - start;
        gap: 0.5rem;
    }

    .feedback - avatar {
        width: 40px; /* Smaller avatar on mobile */
        height: 40px;
    }

    .feedback - actions {
        justify - content: flex - start; /* Align buttons to the left on mobile */
        gap: 0.5rem; /* Smaller gap */
    }

    .feedback - actions.btn {
        min - width: 70px; /* Smaller buttons */
        font - size: 0.75rem; /* Smaller text */
        padding: 0.25rem 0.75rem; /* Smaller padding */
    }
}