<script setup>
import { ref, onMounted } from 'vue';
//state
const counter = ref(0);
// methods
function increment(num) {
 counter.value = num++;
 }
function decrement(num) {
 counter.value = num--;
}
//lifecycle hooks
onMounted(() => {
 alert('Application started');
})
</script>