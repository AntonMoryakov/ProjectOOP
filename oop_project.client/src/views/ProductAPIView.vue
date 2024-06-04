<template>
    <div id="app">

        <ul>
            <h1>Магазин продуктов</h1>
            <form @submit.prevent="createProduct(product)">
                <label for="productId">ID:</label>
                <input type="number" id="productId" v-model="product.id" required>

                <label for="productName">Name:</label>
                <input type="text" id="productName" v-model="product.name" required>

                <label for="productPrice">Price:</label>
                <input type="number" id="productPrice" v-model="product.price" required>

                <button type="submit">Submit</button>
            </form>
            <li v-for="product in products" :key="product.id">
                {{ product.name }} - {{ product.price }} руб.
                <button @click="updateProduct(product)">Изменить</button>
                <button @click="deleteProduct(product.id)">Удалить</button>
            </li>
        </ul>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            apiUrl: '/api/Products',
            products: [],
            product: {
                "id": null,
                "name": "",
                "price": null
            }
        };
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.getProducts();
    },
    methods: {
        async getProducts() {
            try {
                const response = await axios.get(this.apiUrl);
                this.products = response.data;
                console.log(response.data);
            } catch (error) {
                console.error(error);
            }
        },
        async createProduct(product) {
            try {
                const response = await axios.post(this.apiUrl, product);
                this.products.push(response.data);
                console.log(response.data);
            } catch (error) {
                console.error(error);
            }
        },
        async updateProduct(id, product) {
            try {
                const response = await axios.put(`${this.apiUrl}/${id}`, product);
                console.log(response.data);
            } catch (error) {
                console.error(error);
            }
        },
        async deleteProduct(id) {
            try {
                const response = await axios.delete(`${this.apiUrl}/${id}`);
                console.log(response.data);
                this.getProducts();
            } catch (error) {
                console.error(error);
            }
        }
    }
};
</script>

<style>
/* Стили по желанию */
</style>