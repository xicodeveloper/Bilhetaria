
    async function searchMovies() {
    const query = document.getElementById('movieSearch').value.trim();
    const resultsContainer = document.getElementById('searchResults');

    if (query.length < 2) {
    resultsContainer.innerHTML = '';
    return;
}

    const apiKey = '88c2011578e30a692349e50d3119755c';
    const url = `https://api.themoviedb.org/3/search/movie?api_key=${apiKey}&query=${encodeURIComponent(query)}&language=pt-BR`;

    try {
    const response = await fetch(url);
    const data = await response.json();

    if (data.results && data.results.length > 0) {
    resultsContainer.innerHTML = data.results.slice(0, 5).map(movie => `
                    <div class="result-item">
                        <a href="/movie/${movie.id}">
                            <img src="https://image.tmdb.org/t/p/w92${movie.poster_path}" alt="${movie.title}" />
                            <span>${movie.title}</span>
                        </a>
                    </div>
                `).join('');
} else {
    resultsContainer.innerHTML = '<div class="no-results">Nenhum resultado encontrado</div>';
}
} catch (err) {
    console.error('Erro ao buscar filmes:', err);
    resultsContainer.innerHTML = '<div class="error">Erro ao buscar</div>';
}
}
