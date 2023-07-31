// Ambil JWT dari localStorage dan tampilkan nama dan email di halaman
function showUserInfo() {
    const jwt = localStorage.getItem('JWToken');
    if (jwt) {
        // Dekode JWT untuk mendapatkan nama dan email
        const decodedJwt = parseJwt(jwt);
        const name = decodedJwt.name;
        const email = decodedJwt.email;

        // Tampilkan nama dan email di elemen HTML yang sesuai
        document.getElementById('userName').textContent = name;
        document.getElementById('userEmail').textContent = email;
    }
}

// Fungsi untuk mendekode JWT (bisa menggunakan library jwt-decode atau implementasi sendiri)
function parseJwt(token) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}
