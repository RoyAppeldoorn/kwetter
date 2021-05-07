<template>
  <div class="mx-auto max-w-7xl">
    <div class="grid grid-cols-3 gap-14">
      <LeftPanel>
        <slot name="leftPanel" />
      </LeftPanel>
      <MiddlePanel>
        <slot name="middlePanel" />
      </MiddlePanel>
      <RightPanel>
        <slot name="rightPanel" />
      </RightPanel>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import LeftPanel from '@/modules/layouts/LeftPanel.vue';
import MiddlePanel from '@/modules/layouts/MiddlePanel.vue';
import RightPanel from '@/modules/layouts/RightPanel.vue';

export default defineComponent({
  components: {
    LeftPanel,
    MiddlePanel,
    RightPanel,
  },
});
</script>

<style lang="postcss">
body {
  @apply bg-gray-900 text-white;
}
</style>
