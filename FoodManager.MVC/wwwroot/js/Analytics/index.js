const loadProductCategoriesFilters = () => {
    const container = $("#categoryFilterList");

    $.ajax({
        url: `/ProductCategories/GetProductCategories`,
        type: "get",
        success: function (data) {
            setupCategoryFilter(data, container);
        },
        error: function () {
            console.error("Failed to load product categories");
        }
    });
};

const setupCategoryFilter = (categories, container) => {
    container.empty();

    const translatedCategories = categories.map(category => ({
        translatedName: category.translationKey
            ? getTranslation(category.translationKey).toLocaleLowerCase()
            : category.name,
        value: category.id
    }));

    const uncategorizedCategory = {
        translatedName: getTranslation("Uncategorized").toLocaleLowerCase(),
        value: -1
    };

    translatedCategories.push(uncategorizedCategory);
    translatedCategories.sort((a, b) => a.translatedName.localeCompare(b.translatedName));

    translatedCategories.forEach(category => {
        container.append(`
            <label class="list-group-item d-flex justify-content-between align-items-center">
                <span>${category.translatedName}</span>
                <input class="form-check-input" type="checkbox" name="CategoryIds" value="${category.value}" 
                       ${selectedCategoryIds?.includes(String(category.value)) ? "checked" : ""}/>
            </label>
        `);
    });
};

const chartEl = document.getElementById("expChart");
let chartInstance = null;

const renderAnalytics = (data) => {
    const tableBody = $("#expTable tbody");
    const totalSpan = $("#totalSpan");
    tableBody.empty();

    data.points.forEach((p, idx) => {
        const date = new Date(p.date);
        const formatted = date.toLocaleDateString();
        const row = `
            <tr data-index="${idx}">
                <td>${formatted}</td>
                <td class="text-end">${p.count}</td>
            </tr>`;
        tableBody.append(row);
    });

    totalSpan.text(data.total);

    const labels = data.points.map(p => p.date);
    const values = data.points.map(p => p.count);

    if (chartInstance) chartInstance.destroy();
    chartInstance = new Chart(chartEl, {
        type: "line",
        data: {
            labels,
            datasets: [{
                label: getTranslation("ExpiringProducts"),
                data: values,
                tension: 0.25,
                borderWidth: 2,
                pointRadius: 3
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: { ticks: { autoSkip: true, maxRotation: 0 } },
                y: { beginAtZero: true, precision: 0 }
            },
            plugins: {
                legend: { display: false },
                tooltip: {
                    intersect: false,
                    mode: "nearest",
                    callbacks: {
                        label: (ctx) => `${getTranslation("ExpiringProducts")}: ${ctx.parsed.y}`
                    }
                }
            }
        }
    });
};

const loadAnalyticsData = () => {
    const from = $("#fromInput").val() || initialFrom;
    const to = $("#toInput").val() || initialTo;
    const checked = $("#categoryFilterList input[name='CategoryIds']:checked")
        .map(function () { return $(this).val(); })
        .get();

    const qs = new URLSearchParams();
    qs.set("From", from);
    qs.set("To", to);
    checked.forEach(id => qs.append("CategoryIds", id));

    $.ajax({
        url: `/Analytics/ExpiringCount?${qs.toString()}`,
        type: "get",
        success: function (data) {
            renderAnalytics(data);
        },
        error: function () {
            toastr?.error(getTranslation("FetchError") || "Failed to load analytical data.");
        }
    });
};

const setupDateValidation = () => {
    const fromInput = document.getElementById("fromInput");
    const toInput = document.getElementById("toInput");

    const validateDates = () => {
        const from = new Date(fromInput.value);
        const to = new Date(toInput.value);

        toInput.min = fromInput.value;

        if (to < from) {
            toInput.value = fromInput.value;
        }
    };

    fromInput.addEventListener("change", validateDates);
    toInput.addEventListener("change", validateDates);
};

const setupFiltersForm = () => {
    $("#filtersModal form").on("submit", function (e) {
        e.preventDefault();

        const from = $("#fromInput").val();
        const to = $("#toInput").val();

        const formatDate = (iso) => {
            const d = new Date(iso);
            const day = String(d.getDate()).padStart(2, "0");
            const month = String(d.getMonth() + 1).padStart(2, "0");
            const year = d.getFullYear();
            return `${day}.${month}.${year}`;
        };

        const display = `${formatDate(from)} → ${formatDate(to)}`;
        $("input.form-control[readonly]").val(display);

        loadAnalyticsData();

        const modalEl = document.getElementById("filtersModal");
        const modal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
        modal.hide();
    });
};

$(document).ready(function () {
    if (!$("#fromInput").val()) {
        const today = new Date();
        $("#fromInput").val(today.toISOString().split("T")[0]);
        const in30 = new Date();
        in30.setDate(today.getDate() + 30);
        $("#toInput").val(in30.toISOString().split("T")[0]);
    }

    loadProductCategoriesFilters();
    setupFiltersForm();
    setupDateValidation();
    loadAnalyticsData();
});
