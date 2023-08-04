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
document.addEventListener('DOMContentLoaded', function () {
    const tableBody = document.querySelector('tbody');
    tableBody.addEventListener('click', function (event) {
        const deleteButton = event.target.closest('.delete-button');
        if (deleteButton) {
            event.preventDefault();
            const form = deleteButton.closest('form');
            const guid = form.querySelector('input[name="guid"]').value;

            Swal.fire({
                title: 'Are you sure?',
                text: 'You won\'t be able to recover this data!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'No, cancel',
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
            }).then((result) => {
                if (result.isConfirmed) {
                    // If the user confirms, submit the form
                    Swal.fire(
                        'Deleted!',
                        'Your data has been Deleted.',
                        'success'
                    ).then(() => {
                        form.submit();
                    });
                }
            });
        }
    });
});

