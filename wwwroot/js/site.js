// Lumière Livres — Main JS

document.addEventListener('DOMContentLoaded', function () {

    // ---- Update Cart Count ----
    function updateCartCount() {
        fetch('/panier/Count')
            .then(r => r.json())
            .then(data => {
                const badge = document.getElementById('cartCount');
                if (badge) {
                    badge.textContent = data.count;
                    badge.style.display = data.count > 0 ? 'inline-flex' : 'none';
                }
            })
            .catch(() => {});
    }
    updateCartCount();

    // ---- Auto-dismiss Toast ----
    const toast = document.getElementById('toast');
    if (toast) {
        setTimeout(() => { toast.style.display = 'none'; }, 3800);
    }

    // ---- Quantity picker (detail page) ----
    window.changeQty = function (delta) {
        const input = document.getElementById('qty');
        if (!input) return;
        const val = parseInt(input.value || '1') + delta;
        input.value = Math.max(1, Math.min(10, val));
    };

    // ---- Shipping option toggle (checkout) ----
    document.querySelectorAll('.shipping-option, .payment-option').forEach(opt => {
        opt.addEventListener('click', function () {
            const group = this.closest('.shipping-options, .payment-options');
            if (group) {
                group.querySelectorAll('.shipping-option, .payment-option').forEach(o => {
                    o.classList.remove('shipping-option--selected', 'payment-option--selected');
                });
                this.classList.add(
                    this.classList.contains('shipping-option') ? 'shipping-option--selected' : 'payment-option--selected'
                );
            }
            const radio = this.querySelector('input[type="radio"]');
            if (radio) radio.checked = true;
        });
    });

    // ---- Smooth scroll on anchor links ----
    document.querySelectorAll('a[href^="#"]').forEach(a => {
        a.addEventListener('click', function (e) {
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        });
    });

    // ---- Author avatar fallback ----
    document.querySelectorAll('.author-avatar img').forEach(img => {
        img.addEventListener('error', function () {
            this.style.display = 'none';
            const fallback = this.nextElementSibling;
            if (fallback) fallback.style.display = 'flex';
        });
    });

    // ---- Animate elements on scroll ----
    if ('IntersectionObserver' in window) {
        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        document.querySelectorAll('.book-card, .trust-item, .category-chip').forEach(el => {
            el.classList.add('fade-in');
            observer.observe(el);
        });
    }
});

// ---- Auth: image fallback for authors ----
document.querySelectorAll('.author-list-photo img, .author-detail-photo').forEach(img => {
    img.addEventListener('error', function () {
        this.style.display = 'none';
        const fb = this.nextElementSibling;
        if (fb) fb.style.display = 'flex';
    });
});

// ---- Highlight active nav link ----
(function () {
    const path = window.location.pathname;
    document.querySelectorAll('.nav-link').forEach(link => {
        if (link.getAttribute('href') && path.startsWith(link.getAttribute('href')) && link.getAttribute('href') !== '/') {
            link.style.color = 'var(--charcoal)';
            link.style.fontWeight = '600';
        }
    });
})();
