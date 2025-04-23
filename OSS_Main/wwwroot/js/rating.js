  const stars = document.querySelectorAll('.star-icon');
    const ratingInput = document.getElementById('ratingInput');

    stars.forEach(star => {
        star.addEventListener('click', () => {
            const selectedValue = parseInt(star.getAttribute('data-value'));
            ratingInput.value = selectedValue;

            stars.forEach(s => {
                const starValue = parseInt(s.getAttribute('data-value'));
                if (starValue <= selectedValue) {
                    s.classList.add('selected');
                } else {
                    s.classList.remove('selected');
                }
            });
        });
    });