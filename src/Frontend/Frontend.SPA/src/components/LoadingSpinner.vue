<script lang="ts">
import { defineComponent, h, toRefs } from 'vue';

export default defineComponent({
  name: 'LoadingSpinner',
  props: {
    size: {
      type: [String, Number],
      default: '64px',
    },
    color: {
      type: String as () => 'primary' | 'white',
      default: 'primary',
    },
  },
  setup(props) {
    const { size } = toRefs(props);

    return () =>
      h('div', {
        class: ['loading-spinner', 'inline-block'],
        style: {
          '--size': size.value,
          '--borderColor': '#FF3146 rgba(39, 149, 217, 0.2) #FF3146 rgba(39, 149, 217, 0.2)',
        },
      });
  },
});
</script>

<style scoped>
.loading-spinner {
  width: var(--size);
  height: var(--size);
}
.loading-spinner:after {
  content: ' ';
  display: block;
  width: var(--size);
  height: var(--size);
  margin: 8px;
  border-radius: 50%;
  border: 6px solid;
  border-color: var(--borderColor);
  animation: spin 1.2s linear infinite;
}
@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
