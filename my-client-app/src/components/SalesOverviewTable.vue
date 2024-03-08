<template>
    <table v-if="state.salesData && state.salesData.length > 0" class="box-shadow">
        <thead>
            <tr>
                <th>Territory</th>
                <th>Shipped Orders</th>
                <th>Cancelled Orders</th>
                <th>Average Ship Days</th>
                <th>Average Due Days</th>
                <th>In-Person Orders</th>
                <th>Online Orders</th>
                <th>Ordered Products</th>
                <th>Shipping Cost Total</th>
                <th>Orders Sub Total</th>
                <th>Orders Tax Amount Total</th>
                <th>Orders Total Due</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="salesData in paginatedSalesData" :key="salesData.territory">
                <td>{{ salesData.territory }}</td>
                <td>{{ integerFormatter.format(salesData.shippedOrders) }}</td>
                <td>{{ integerFormatter.format(salesData.cancelledOrders) }}</td>
                <td>{{ integerFormatter.format(salesData.averageShipDays) }}</td>
                <td>{{ integerFormatter.format(salesData.averageDueDays) }}</td>
                <td>{{ integerFormatter.format(salesData.inPersonOrders) }}</td>
                <td>{{ integerFormatter.format(salesData.onlineOrders) }}</td>
                <td>{{ integerFormatter.format(salesData.orderedQuantity) }}</td>
                <td>{{ currencyFormatter.format(salesData.shippingCost) }}</td>
                <td>{{ currencyFormatter.format(salesData.subTotal) }}</td>
                <td>{{ currencyFormatter.format(salesData.taxAmount) }}</td>
                <td>{{ currencyFormatter.format(salesData.totalDue) }}</td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="12">
                    <div class="pagination">
                        <button @click="goToPage(state.currentPage - 1)" :disabled="state.currentPage === 1"
                            style="border: 0">
                            &lt;
                        </button>
                        <span v-for="page in visiblePages" :key="page" @click="goToPage(page)"
                            :class="{ active: page === state.currentPage }" class="page-number">
                            {{ page }}
                        </span>
                        <button @click="goToPage(state.currentPage + 1)" :disabled="state.currentPage === totalPages"
                            style="border: 0">
                            &gt;
                        </button>
                    </div>
                </td>
            </tr>
        </tfoot>
    </table>
    <table v-else class="box-shadow">
        <thead>
            <tr>
                <th>Territory</th>
                <th>Shipped Orders</th>
                <th>Cancelled Orders</th>
                <th>Average Ship Days</th>
                <th>Average Due Days</th>
                <th>In-Person Orders</th>
                <th>Online Orders</th>
                <th>Ordered Products</th>
                <th>Shipping Cost</th>
                <th>Sub Total</th>
                <th>Tax Amount</th>
                <th>Total Due</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td colspan="12">No results found.</td>
            </tr>
        </tbody>
    </table>
</template>

<script setup>
import { computed, reactive, defineProps } from "vue";

const currencyFormatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' });
const integerFormatter = new Intl.NumberFormat('en-US');

const props = defineProps({
    salesData: Array,
});

const state = reactive({
    salesData: computed(() => props.salesData),
    pageSize: 20,
    currentPage: 1,
});

const paginatedSalesData = computed(() => {
    const startIndex = (state.currentPage - 1) * state.pageSize;
    const endIndex = startIndex + state.pageSize;
    return state.salesData.slice(startIndex, endIndex);
});

const totalPages = computed(() =>
    Math.ceil(state.salesData.length / state.pageSize)
);

const visiblePages = computed(() => {
    const range = 4;
    const start = Math.max(1, state.currentPage - range);
    const end = Math.min(totalPages.value, start + range * 2);

    const result = [];
    for (let i = start; i <= end; i++) {
        result.push(i);
    }
    return result;
});

function goToPage(page) {
    state.currentPage = page;
}
</script>

<style>
table {
    width: 100%;
    border-spacing: 0;
    border: 2px solid #ddd;
    border-radius: 8px;
    overflow: auto;
    align-self: flex-start;
    display: block;
}

th,
td {
    border-bottom: 1px solid #ddd;
    padding: 8px;
    text-align: center;
}

th {
    background-color: #f2f2f2;
}

tfoot {
    background-color: #f2f2f2;
}

.pagination {
    margin-top: 10px;
    display: flex;
    justify-content: center;
    align-items: center;
}

button {
    cursor: pointer;
    padding: 8px;
}

.page-number {
    padding: 8px;
    cursor: pointer;
}

.page-number.active {
    background-color: #181723;
    color: #fff;
    border-color: #181723;
    border-radius: 4px;
}
</style>
